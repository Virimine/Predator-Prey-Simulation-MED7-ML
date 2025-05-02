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

			Debug.LogWarning(itemName + " " + isLocked);
			rectTransform.localPosition = Vector3.zero;
			//ClearDropTarget();
			return;
		}

		Debug.LogWarning(itemName + " here");
		base.ResetDrag();
		SetDragInteractivity(true);
	}
}