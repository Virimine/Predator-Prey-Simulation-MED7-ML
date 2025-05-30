using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FunctionalityDrag : FurnitureDrag {


	public bool isLocked { get; set; }

	public override void OnBeginDrag(PointerEventData eventData) {
		if (isLocked) { return; }
		base.OnBeginDrag(eventData);
	}

	public override void OnDrag(PointerEventData data) {
		if (isLocked) { return; }
		base.OnDrag(data);
	}

	public override void OnEndDrag(PointerEventData eventData) {

		if (isLocked) { return; }
		base.OnEndDrag(eventData);
	}

	public override void ResetDrag() {

		if (isLocked) {

			Debug.Log(itemName + " is Locked: " + isLocked);

			rectTransform.localPosition = Vector3.zero;
			//SetDragInteractivity(true);
			return;
		}

		base.ResetDrag();
	}
}