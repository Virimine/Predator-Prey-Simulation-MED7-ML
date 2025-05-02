using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureDrop : MonoBehaviour, IDropHandler {

	RectTransform rectTransform;

	public SlotType slotType;

	public FurnitureDrag currentItem { get; set; }


	public bool isOccupied => currentItem != null;

	void Start() => rectTransform = GetComponentInChildren<RectTransform>();

	public virtual void OnDrop(PointerEventData eventData) {

		currentItem = eventData.pointerDrag?.GetComponentInChildren<FurnitureDrag>();  // should not be using null propagation here?

		if (eventData.pointerDrag != null && currentItem.itemType == slotType) {
			currentItem.GetComponent<RectTransform>().position = rectTransform.position;
		}

		OnClassDragDropped();
	}


	// Should be moved to the specific board furniture drop when this becomes and abstract class. It should not be called in EmbeddedFurnitureDrop.
	void OnClassDragDropped() {
		if (currentItem.itemType == SlotType.Class && currentItem is ClassDrag classDrag && classDrag.HasStorredFunctionalities) {

			var availableSlots = FurnitureShopGameManager.instance.interfaceManager.AvailableSlots;
			var storedFuncs = classDrag.StorredFunctionalities;

			for (int i = 0; i < storedFuncs.Count; i++) {
				if (i >= availableSlots.Count) {
					Debug.LogWarning("Not enough available slots to place all functionalities!");
					break;
				}

				FurnitureDrop targetSlot = availableSlots[i];
				FurnitureDrag functionality = storedFuncs[i];

				targetSlot.currentItem = functionality;
				targetSlot.currentItem.targetSlot = targetSlot;

				functionality.GetComponent<RectTransform>().position = targetSlot.rectTransform.position;
				FurnitureShopGameManager.instance.interfaceManager.OccupiedSlots.Add(targetSlot);
			}
		}
	}

	public bool Matches(string expectedName) => isOccupied && currentItem.itemName == expectedName;
}