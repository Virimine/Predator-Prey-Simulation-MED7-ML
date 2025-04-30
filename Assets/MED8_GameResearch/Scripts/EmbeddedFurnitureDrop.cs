using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmbeddedFurnitureDrop : FurnitureDrop {

	[SerializeField] ClassDrag dragParent;

	public override void OnDrop(PointerEventData eventData) {
		base.OnDrop(eventData);

		GameObject dragObject = eventData.pointerDrag;
		var functionalityDrag = dragObject.GetComponentInChildren<FunctionalityDrag>();

		if (functionalityDrag != null & !functionalityDrag.isLocked) {

			functionalityDrag.gameObject.transform.SetParent(this.transform);
			functionalityDrag.isLocked = true;
			functionalityDrag.OnEndDrag(eventData);

			dragParent.StoreFunctionality(functionalityDrag);
		}
	}
}

