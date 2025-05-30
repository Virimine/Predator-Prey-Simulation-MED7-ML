using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {
	[SerializeField] Button proceedButton;
	[SerializeField] Button upgradesButton;
	[SerializeField] Button craftButton;
	[SerializeField] RectTransform upgradePanel;
	[SerializeField] TextMeshProUGUI coinsText;
	[SerializeField] UI_DisplayImage displayImage;

	[SerializeField] List<FurnitureDrag> drags;
	[SerializeField] List<FurnitureDrop> slots;


	public List<FurnitureDrop> OccupiedSlots => slots.Where(slot => slot.isOccupied).ToList(); // Get currently occupied slots only
	public List<FurnitureDrop> AvailableSlots => slots.Where(slot => !slot.isOccupied).ToList(); // Get currently unoccupied slots only

	FurnitureShopGameManager manager => FurnitureShopGameManager.instance;

	void Start() {
		craftButton.onClick.AddListener(() => manager.orderManager.ValidateCraft());
		proceedButton.onClick.AddListener(() => OnProceedButtonPressed());
		upgradesButton.onClick.AddListener(() => OnContinueNextDayPressed());
	}

	public void ShowProceedButton(bool isActive) => proceedButton.gameObject.SetActive(isActive);

	public void OnContinueNextDayPressed() {
		upgradePanel.gameObject.SetActive(false);
		ResetBoard();
		manager.ProceedToNextDay();
	}
	public void ShowUpgradeScreen() => upgradePanel.gameObject.SetActive(true);

	public void DisplayFurniture(Sprite furnitureSprite) => displayImage.DisplayFurniture(furnitureSprite);

	public void DisplayFurniture() => displayImage.DisplayFurniture();

	public void ResetBoard() {

		GetActiveDrags().Clear();

		foreach (var drag in GetActiveDrags()) {

			// remove casting when you make abstract classes
			if (drag is FunctionalityDrag func) {
				func.ResetDrag();
				continue;
			}

			if (drag is ClassDrag classDrag) {
				classDrag.ResetDrag();
			}
		}
	}

	void OnProceedButtonPressed() {

		var dialogueManager = manager.dialogueManager;
		if (dialogueManager.isPlaying) {
			dialogueManager.ContinueDialogue();
			return;
		}

		manager.SetState(GameState.FurnitureAssembly);
	}

	List<FurnitureDrag> GetActiveDrags() {
		List<FurnitureDrag> activeDrags = new();

		foreach (var drag in drags) {

			if (drag.isActiveAndEnabled) {
				activeDrags.Add(drag);
			}
		}

		return activeDrags;
	}
}
