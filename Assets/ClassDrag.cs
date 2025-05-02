using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClassDrag : FurnitureDrag {

	[SerializeField] Button removeFunctionality;
	[SerializeField] GameObject interfaceLayout;
	[SerializeField] int removeCost;


	public List<FunctionalityDrag> StorredFunctionalities { get; private set; }
	public bool HasStorredFunctionalities => StorredFunctionalities.Count > 0;

	protected override void Awake() {
		base.Awake();
		StorredFunctionalities = new();
	}

	protected override void Start() {

		base.Start();
		removeFunctionality.onClick.AddListener(() => ClearStoredFunctionalities());
		removeFunctionality.onClick.AddListener(() => CoinsHandler.SpendCoins(removeCost));
	}

	public override void OnEndDrag(PointerEventData eventData) {

		base.OnEndDrag(eventData);
		CompressLayout();   // move to Interface Drag script
	}

	public void ClearStoredFunctionalities() {

		foreach (var functionality in StorredFunctionalities) {
			functionality.ResetDrag();
			functionality.isLocked = false;
		}

		StorredFunctionalities.Clear();
	}

	// move to Interface Drag script
	public void CompressLayout() {
		if (interfaceLayout == null) { return; }
		interfaceLayout.SetActive(targetSlot == null);
	}
}
