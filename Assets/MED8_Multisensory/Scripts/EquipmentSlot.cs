using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class EquipmentSlot : MonoBehaviour, ISelectHandler, IDeselectHandler {

	[SerializeField] Image image;

	EventSystem eventSystem;
	EquipmentData itemData;
	Button button;
	Animator animator;

	private void Awake() {
		eventSystem = FindAnyObjectByType<EventSystem>();
		animator = GetComponentInChildren<Animator>();
		button = GetComponentInChildren<Button>();
	}

	private void Start() => button.onClick.AddListener((() => UIManager.instance.UpdateElements(itemData)));

	public void Initialize(EquipmentData item) {
		image.sprite = item.sprite;
		itemData = item;
	}

	public void SelectSlot() {
		button.Select();
		ExecuteEvents.Execute(button.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
	}

	public void OnSelect(BaseEventData eventData) {
		if (UIManager.instance.isAnimated) { animator.Play("Hovered"); }
		UIManager.instance.UpdateElements(itemData);
	}

	public void OnDeselect(BaseEventData eventData) {
		if (UIManager.instance.isAnimated) { animator.Play("Default"); }
	}

	public void PlayFadeIn() => animator.Play("Fade In");
}
