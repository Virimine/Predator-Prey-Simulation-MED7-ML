using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType { Class, Functionality, Interface }

public class FurnitureDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField] TextMeshProUGUI nameTMP;
	[SerializeField] TextMeshProUGUI typeTMP;

	public SlotType itemType;
	public string itemName;

	FurnitureDrop dropTarget;
	RectTransform rectTransform;
	CanvasGroup canvas;
	Vector3 initalPosition;


	protected virtual void Awake() {
		rectTransform = GetComponentInChildren<RectTransform>();
		canvas = GetComponentInChildren<CanvasGroup>();
	}
	void Start() {
		initalPosition = rectTransform.position;
		nameTMP.text = DialogueFormatter.SplitCamelCase(itemName);
		typeTMP.text = DialogueFormatter.SplitCamelCase(itemType.ToString());
	}


	public virtual void OnBeginDrag(PointerEventData eventData) {

		canvas.alpha = 0.8f;
		canvas.blocksRaycasts = false;

		if (dropTarget != null) {
			dropTarget.ClearSlot();
			dropTarget = null;
		}
	}

	public virtual void OnDrag(PointerEventData data) {

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, data.position, data.pressEventCamera, out var globalMousePos)) {
			rectTransform.position = globalMousePos;
		}
	}

	public virtual void OnEndDrag(PointerEventData eventData) {
		canvas.alpha = 1f;
		canvas.blocksRaycasts = true;


		if (eventData.pointerEnter == null) {
			rectTransform.position = initalPosition;
			return;
		}

		dropTarget = eventData.pointerEnter.GetComponentInChildren<FurnitureDrop>();
		bool isCorrectDrop = dropTarget != null && itemType == dropTarget.slotType;

		if (!isCorrectDrop) {
			rectTransform.position = initalPosition;
		}
	}

	public void ResetDrag() {

		rectTransform.SetParent(FurnitureShopGameManager.instance.mainCanvas.transform);
		rectTransform.position = initalPosition;


		if (dropTarget != null) {
			dropTarget.ClearSlot();
			dropTarget = null;
		}
	}


}

