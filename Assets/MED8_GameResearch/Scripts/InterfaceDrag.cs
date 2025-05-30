using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InterfaceDrag : FurnitureDrag, IFunctionalityStorer {

	[SerializeField] Button removeFunctionality;
	[SerializeField] GameObject interfaceLayout;
	[SerializeField] int removeCost;

	public List<FunctionalityDrag> StorredFunctionalities { get; private set; }
	public bool HasStorredFunctionalities => StorredFunctionalities.Count > 0;

	public string ItemName => itemName;

	FurnitureDrag parentDrag;

	protected override void Awake() {
		base.Awake();
		StorredFunctionalities = new();
	}

	protected override void Start() {
		base.Start();
		removeFunctionality.onClick.AddListener(() => ClearStoredFunctionalities());
	}

	public override void OnEndDrag(PointerEventData eventData) {

		base.OnEndDrag(eventData);
		parentDrag = GetComponentInParent<ClassDrag>();
		CompressLayout();
	}


	public void StoreFunctionality(FunctionalityDrag drag) {
		if (!StorredFunctionalities.Contains(drag)) {
			StorredFunctionalities.Add(drag);
		}
	}

	public bool IsFunctionalityAlreadyStored(FunctionalityDrag drag) => StorredFunctionalities.Contains(drag);

	public IFunctionalityStorer GetParentStorer() => parentDrag as IFunctionalityStorer;

	public void ClearStoredFunctionalities() {

		if (CoinsHandler.coins < removeCost) { return; }

		foreach (var functionality in StorredFunctionalities) {
			functionality.isLocked = false;
			functionality.ResetDrag();
		}

		StorredFunctionalities.Clear();
		CoinsHandler.SpendCoins(removeCost);
	}

	void CompressLayout() => interfaceLayout.SetActive(targetSlot == null);
}