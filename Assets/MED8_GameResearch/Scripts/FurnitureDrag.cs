using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { Class, Functionality, Interface }

public class FurnitureDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField] TextMeshProUGUI nameTMP;
	[SerializeField] TextMeshProUGUI typeTMP;

	CanvasGroup canvas;
	Vector3 initalPosition;

	protected RectTransform rectTransform;

	public SlotType itemType;
	public string itemName; // make FunctionalityType for functionality drags and Type for Class / Interface.
	public FurnitureDrop targetSlot { get; set; }

	protected virtual void Awake() {
		rectTransform = GetComponentInChildren<RectTransform>();
		canvas = GetComponentInChildren<CanvasGroup>();
	}
	protected virtual void Start() {
		initalPosition = rectTransform.position;
		nameTMP.text = DialogueFormatter.SplitCamelCase(itemName);
		typeTMP.text = DialogueFormatter.SplitCamelCase(itemType.ToString());
	}


	public virtual void OnBeginDrag(PointerEventData eventData) {
		SetDragInteractivity(false);
		ClearDropTarget();
	}

	public virtual void OnDrag(PointerEventData data) {

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, data.position, data.pressEventCamera, out var globalMousePos)) {
			rectTransform.position = globalMousePos;
		}
	}

	public virtual void OnEndDrag(PointerEventData eventData) {

		SetDragInteractivity(true);

		targetSlot = eventData.pointerEnter?.GetComponentInChildren<FurnitureDrop>(); // should not be using null propagation here?

		// Check if the drop target exists and if the item matches the expected slot type.
		bool isCorrectDrop = targetSlot != null && itemType == targetSlot.slotType;

		// If nothing was under the pointer when the drag ended or if it's not a valid drop, reset the object's position.
		if (!isCorrectDrop) {
			rectTransform.position = initalPosition;
		}
	}

	public virtual void ResetDrag() {

		var canvas = FurnitureShopGameManager.instance.mainCanvas.transform;
		rectTransform.SetParent(canvas); // Set to the second-to-last child of a parent transform.
		rectTransform.SetSiblingIndex(canvas.childCount - 5);

		rectTransform.position = initalPosition;

		ClearDropTarget();
	}

	// If it is was placed in a slot, clear it and set it to null.
	protected void ClearDropTarget() {

		if (targetSlot == null) { return; }

		targetSlot.currentItem = null;
		targetSlot = null;
	}

	protected void SetDragInteractivity(bool blocksRaycasts) {
		canvas.alpha = blocksRaycasts ? 1f : 0.8f;
		canvas.blocksRaycasts = blocksRaycasts;
	}
}

