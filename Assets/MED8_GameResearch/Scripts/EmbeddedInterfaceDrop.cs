using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmbeddedInterfaceDrop : FurnitureDrop {

	[SerializeField] ClassDrag parentDrag;

	public override void OnDrop(PointerEventData eventData) {
		base.OnDrop(eventData);

		var interfaceDrag = eventData.pointerDrag.GetComponentInChildren<InterfaceDrag>();
		if (interfaceDrag == null) { return; }

		interfaceDrag.gameObject.transform.SetParent(this.transform);
		parentDrag.itemName = interfaceDrag.itemName;

		StoreFunctionality(interfaceDrag);
	}

	public void StoreFunctionality(InterfaceDrag interfaceDrag) => parentDrag.StorredFunctionalities.AddRange(interfaceDrag.StorredFunctionalities);
}
