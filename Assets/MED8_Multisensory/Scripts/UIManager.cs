using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] List<EquipmentData> items;
	[SerializeField] EquipmentSlot slot;
	[SerializeField] Transform gridPanel;
	[SerializeField] DescriptionPanel descriptionPanel;
	[SerializeField] Image mainImage;
	[SerializeField] Animator anim;
	[SerializeField] float slotTimer;

	List<EquipmentSlot> slots = new List<EquipmentSlot>();

	public static UIManager instance;

	public bool isAnimated { get; private set; }

	bool isOpen = false;

	void Awake() {

		if (instance == null) { instance = this; }
	}

	void Start() => UpdateElements(items[0]);

	void Update() {

		if (Input.GetKeyDown(KeyCode.Alpha1) && !isOpen) { OpenAnimated(); }
		if (Input.GetKeyDown(KeyCode.Alpha2) && !isOpen) { OpenStatic(); }
		if (Input.GetKeyDown(KeyCode.Alpha3) && isOpen) { Close(); }
	}

	public void UpdateElements(EquipmentData item) {
		mainImage.sprite = item.sprite;
		descriptionPanel.UpdateTexts(item);
	}

	void OpenAnimated() {
		isOpen = true;
		isAnimated = true;

		anim.Play("UI_Open_Animated");
		StartCoroutine(PlaySlotsFadeIn());
	}

	void OpenStatic() {
		isOpen = true;

		anim.Play("UI_Open_Static");

		foreach (var item in items) {

			var newSlot = Instantiate(slot, gridPanel);
			newSlot.Initialize(item);
			slots.Add(newSlot);
		}

		slots[0].SelectSlot();
	}

	void Close() {
		isOpen = false;
		isAnimated = false;

		anim.Play("UI_Close");
		foreach (var slot in slots) { Destroy(slot.gameObject); }
		slots.Clear();
	}

	IEnumerator PlaySlotsFadeIn() {

		yield return new WaitForSeconds(slotTimer);

		foreach (var item in items) {

			var newSlot = Instantiate(slot, gridPanel);
			newSlot.Initialize(item);
			newSlot.PlayFadeIn();
			slots.Add(newSlot);
		}

		slots[0].SelectSlot();
	}
}
