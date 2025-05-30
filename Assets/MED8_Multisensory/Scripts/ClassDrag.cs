using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClassDrag : FurnitureDrag, IFunctionalityStorer {

	[SerializeField] Button removeFunctionality;
	[SerializeField] int removeCost;


	public List<FunctionalityDrag> StorredFunctionalities { get; private set; }
	public bool HasStorredFunctionalities => StorredFunctionalities.Count > 0;

	public string ItemName { get; set; }

protected override void Awake() {
		base.Awake();
		StorredFunctionalities = new();
	}

	protected override void Start() {
		base.Start();

		removeFunctionality.onClick.AddListener(() => ClearStoredFunctionalities());
	}

	public void ClearStoredFunctionalities() {

		if (CoinsHandler.coins < removeCost) { return; }

		foreach (var functionality in StorredFunctionalities) {
			functionality.isLocked = false;
			functionality.ResetDrag();
		}

		StorredFunctionalities.Clear();
		CoinsHandler.SpendCoins(removeCost);
	}

	public void StoreFunctionality(FunctionalityDrag drag) {
		if (!StorredFunctionalities.Contains(drag)) {
			StorredFunctionalities.Add(drag);
		}
	}
	public bool IsFunctionalityAlreadyStored(FunctionalityDrag drag) => StorredFunctionalities.Contains(drag);


	public override void OnDroppedInto(FurnitureDrop targetSlot) {
		if (HasStorredFunctionalities) {
			var availableSlots = FurnitureShopGameManager.instance.interfaceManager.AvailableSlots;
			var storedFuncs = StorredFunctionalities;

			for (int i = 0; i < storedFuncs.Count; i++) {
				if (i >= availableSlots.Count) {
					Debug.LogWarning("Not enough available slots to place all functionalities!");
					break;
				}

				var slot = availableSlots[i];
				var func = storedFuncs[i];

				slot.currentDrag = func;
				slot.currentDrag.targetSlot = slot;
				func.GetComponent<RectTransform>().position = slot.rectTransform.position;

				FurnitureShopGameManager.instance.interfaceManager.OccupiedSlots.Add(slot);
			}
		}
	}
}
