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

	List<FurnitureDrag> drags;
	List<FurnitureDrop> slots;

	public List<FurnitureDrop> OccupiedSlots => slots.Where(slot => slot.isOccupied).ToList(); // Get currently occupied slots only

	public event System.Action OnOpenShopButtonPressed;

	FurnitureShopGameManager manager => FurnitureShopGameManager.instance;

	void Awake() {
		drags = FindObjectsByType<FurnitureDrag>(FindObjectsSortMode.None).ToList();
		slots = FindObjectsByType<FurnitureDrop>(FindObjectsSortMode.None).ToList();
	}

	void Start() {
		craftButton.onClick.AddListener(() => manager.orderManager.ValidateCraft());
		proceedButton.onClick.AddListener(() => OnProceedButtonPressed());
		upgradesButton.onClick.AddListener(() => OnContinueNextDayPressed());
		CoinsHandler.OnCoinsChanged += UpdateCoinsText;

	}

	public void ShowProceedButton(bool isActive) {
		proceedButton.gameObject.SetActive(isActive);
	}


	public void OnContinueNextDayPressed() {
		upgradePanel.gameObject.SetActive(false);
		ResetBoard();
		manager.ProceedToNextDay();
	}
	public void ShowUpgradeScreen() {
		upgradePanel.gameObject.SetActive(true);
	}

	public void UpdateCoinsText(int newAmount) => coinsText.text = $"coins: {newAmount}";

	public void ResetBoard(){

		foreach (var drag in drags) {
			drag.ResetDrag();
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
}
