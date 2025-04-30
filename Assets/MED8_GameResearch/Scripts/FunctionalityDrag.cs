using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FunctionalityDrag : FurnitureDrag {

	public virtual bool isLocked { get; set; }

	public override void OnBeginDrag(PointerEventData eventData) {
		if (isLocked) { return; }
		base.OnBeginDrag(eventData);
	}

	public override void OnDrag(PointerEventData data) {
		if (isLocked) { return; }
		base.OnDrag(data);
	}

	public override void OnEndDrag(PointerEventData eventData) {

		// QUICK PATCH: OnEndDrag is run after drop OnDrop. This makes drags stay uninteractable.
		var canvas = GetComponentInChildren<CanvasGroup>();
		canvas.alpha = 1f;
		canvas.blocksRaycasts = true;

		if (isLocked) { return; }
		base.OnEndDrag(eventData);
	}
}