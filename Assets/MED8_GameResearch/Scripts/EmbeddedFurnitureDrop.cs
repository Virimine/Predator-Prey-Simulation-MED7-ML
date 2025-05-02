using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmbeddedFurnitureDrop : FurnitureDrop {

	[SerializeField] ClassDrag dragParent;

	public override void OnDrop(PointerEventData eventData) {
		base.OnDrop(eventData);

		currentItem = eventData.pointerDrag.GetComponentInChildren<FunctionalityDrag>();


		var classDrag = eventData.pointerDrag.GetComponentInChildren<ClassDrag>();
		if (classDrag != null && classDrag.itemType == SlotType.Interface) {
			classDrag.gameObject.transform.SetParent(this.transform);
			classDrag.CompressLayout();
			dragParent.StorredFunctionalities.AddRange(classDrag.StorredFunctionalities);
			dragParent.itemName = classDrag.itemName;
			Debug.LogWarning("heere		");
			return;
		}

		var functionalityDrag = eventData.pointerDrag.GetComponentInChildren<FunctionalityDrag>();
		if (functionalityDrag != null && !functionalityDrag.isLocked) {

			functionalityDrag.gameObject.transform.SetParent(this.transform);
			functionalityDrag.isLocked = true;
			functionalityDrag.OnEndDrag(eventData);
			Debug.Log(functionalityDrag.itemName + " " + functionalityDrag.isLocked);
			 
			dragParent.StorredFunctionalities.Add(functionalityDrag);
		}
	}
}

