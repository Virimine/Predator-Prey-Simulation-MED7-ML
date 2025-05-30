using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class FurnitureDrop : MonoBehaviour, IDropHandler {

	public SlotType slotType;

	public FurnitureDrag currentDrag { get; set; }

	public bool isOccupied => currentDrag != null;

	public RectTransform rectTransform { get; set; }  // make private

	void Start() => rectTransform = GetComponentInChildren<RectTransform>();

	public virtual void OnDrop(PointerEventData eventData) {

		if (eventData.pointerDrag == null) { return; }

		currentDrag = eventData.pointerDrag.GetComponentInChildren<FurnitureDrag>();

		if (currentDrag == null || currentDrag.itemType != slotType) { return; }

		currentDrag.GetComponent<RectTransform>().position = rectTransform.position;
		currentDrag.OnDroppedInto(this);
	}

	public bool Matches(string expectedName) => isOccupied && currentDrag.itemName == expectedName;
}