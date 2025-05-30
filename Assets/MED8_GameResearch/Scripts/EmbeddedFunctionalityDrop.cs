using UnityEngine;
using UnityEngine.EventSystems;

public class EmbeddedFunctionalityDrop : FurnitureDrop {

	[SerializeField] FurnitureDrag parentDrag;

	public override void OnDrop(PointerEventData eventData) {
		base.OnDrop(eventData);

		var functionalityDrag = eventData.pointerDrag.GetComponentInChildren<FunctionalityDrag>();
		if (functionalityDrag == null) { return; }

		functionalityDrag.gameObject.transform.SetParent(this.transform);
		functionalityDrag.isLocked = true;
		functionalityDrag.OnEndDrag(eventData);

		// Check what type of drag this is
		if (parentDrag is InterfaceDrag interfaceDrag) {
			// Store in this interface
			interfaceDrag.StoreFunctionality(functionalityDrag);

			// If this interface is already stored in its parent, bubble it up
			var outerParent = interfaceDrag.GetParentStorer();
			if (outerParent != null && outerParent.IsFunctionalityAlreadyStored(functionalityDrag)) {
				outerParent.StoreFunctionality(functionalityDrag);
			}
		}
		else if (parentDrag is ClassDrag classDrag) {

			// Otherwise, just store it directly
			classDrag.StoreFunctionality(functionalityDrag);
		}
	}
}

