using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour {

	[SerializeField] List<CustomerRequest> requestObjects;



	FurnitureShopGameManager manager => FurnitureShopGameManager.instance;

	public FurnitureData itemData { get; private set; }



	public void TakeOrder() {

			List<CustomerRequest> requests = new List<CustomerRequest>(requestObjects);
			var rndIndex = Random.Range(0, requests.Count);
			var currentRequest = requests[rndIndex];

			manager.dialogueManager.DisplayRequest(currentRequest);

		itemData = currentRequest.furniture;

	}

	// Move to data script
	public List<string> SetRequirements() {

		if (itemData == null) {
			Debug.LogWarning("No recipe assigned!");
			return null;
		}

		List<string> allRequirements = new(); // Build a fresh list of requirements
		allRequirements.Add(itemData.type.ToString());
		allRequirements.AddRange(itemData.functionalities.Select(f => f.ToString()));

		return allRequirements;
	}

	public List<FurnitureDrop> usedSlots = new();
	public List<string> matchedRequirements = new();
	public void ValidateCraft() {

		List<string> remainingRequirements = new List<string>(SetRequirements()); // Make a working copy we can remove from

		usedSlots.Clear();
		matchedRequirements.Clear();

		

		foreach (var slot in manager.interfaceManager.OccupiedSlots) {
			string itemInSlot = slot.currentItem.itemName;

			var matchedReq = remainingRequirements.FirstOrDefault(req => slot.Matches(req));
			if (!string.IsNullOrEmpty(matchedReq)) {
				usedSlots.Add(slot);
				matchedRequirements.Add(matchedReq);
				remainingRequirements.Remove(matchedReq);
			}
			else {
				// Slot doesn't match any requirement = invalid
				PlayWrong();
				return;
			}
		}

		// Check if all requirements were matched
		if (remainingRequirements.Count == 0 && usedSlots.Count == manager.interfaceManager.OccupiedSlots.Count) {
			PlayCorrect();
		}
		else {
			PlayWrong();
		}
	}

	void PlayCorrect() {
		Debug.Log("Correct Craft!");
		CoinsHandler.GainCoins(10);

		manager.interfaceManager.ResetBoard();
		TakeOrder();
	}

	void PlayWrong() {
		Debug.Log("Incorrect Craft!");
		CoinsHandler.SpendCoins(5);
	}
}

