using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureDrop : MonoBehaviour, IDropHandler {

	public SlotType slotType;

	RectTransform rectTransform;

	public FurnitureDrag currentItem { get; private set; }

	public bool isOccupied {
		get => currentItem != null;
		private set { }
	}

	void Start() => rectTransform = GetComponentInChildren<RectTransform>();

	public virtual void OnDrop(PointerEventData eventData) {

		GameObject dragObject = eventData.pointerDrag;
		currentItem = dragObject.GetComponentInChildren<FurnitureDrag>();

		if (dragObject != null && currentItem.itemType == slotType) {
			eventData.pointerDrag.GetComponent<RectTransform>().position = rectTransform.position;
		}
	}

	public bool Matches(string expectedName) => isOccupied && currentItem.itemName == expectedName;

	public string GetItemNameIfOfType() => isOccupied ? currentItem.itemName : null;

	public void ClearSlot() {
		currentItem = null;
		isOccupied = false;
	}
}