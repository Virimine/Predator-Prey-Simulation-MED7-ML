using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { Class, Functionality, Interface }

public abstract class FurnitureDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField] protected TextMeshProUGUI nameTMP;
	[SerializeField] protected TextMeshProUGUI typeTMP;

	CanvasGroup canvas;
	protected Vector3 initalPosition;

	public RectTransform rectTransform { get; set; } // make private

	public SlotType itemType;
	public string itemName;

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

		// If there is no drop below, return to your initial position.
		if (eventData.pointerEnter == null) {

			rectTransform.position = initalPosition;
			return;
		}

		var tempSlot = eventData.pointerEnter?.GetComponent<FurnitureDrop>(); // should not be using null propagation here?

		// Check if the drop target exists and if the item matches the expected slot type, then set the target slot.
		bool isCorrectDrop = tempSlot != null && itemType == tempSlot.slotType;
		if (!isCorrectDrop) {

			// If nothing was under the pointer when the drag ended or if it's not a valid drop, reset the object's position.
			rectTransform.position = initalPosition;
			return;
		}

		targetSlot = tempSlot;
	}

	public virtual void OnDroppedInto(FurnitureDrop targetSlot) { }

	public virtual void ResetDrag() {

		var canvas = FurnitureShopGameManager.instance.mainCanvas.transform;
		rectTransform.SetParent(canvas); // Set to the second-to-last child of a parent transform.
		rectTransform.SetSiblingIndex(canvas.childCount - 5);
	
		rectTransform.position = initalPosition;
	
		ClearDropTarget();
		SetDragInteractivity(true);
	}

	// If it is was placed in a slot, clear it and set it to null.
	protected void ClearDropTarget() {

		if (targetSlot == null) { return; }

		targetSlot.currentDrag = null;
		targetSlot = null;
	}

	protected void SetDragInteractivity(bool IsInteractive) {
		canvas.alpha = IsInteractive ? 1f : 0.8f;
		canvas.blocksRaycasts = IsInteractive;
	}
}

