using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesSystem : MonoBehaviour {

	void Start() {
		UpgradeEvents.OnUpgradePurchased += HandleUpgrade;
	}

	private void HandleUpgrade(UpgradeData upgrade) {
		switch (upgrade.type) {
			case UpgradeType.UnlockClass:
				UnlockClass( upgrade);
				break;
			case UpgradeType.UnlockStyle:
				UnlockStyle(upgrade.styleType);
				break;
			case UpgradeType.UnlockInterface:
				UnlockInterface(upgrade.furnitureType);
				break;
		}
	}

	private void UnlockClass(UpgradeData data) {
		Debug.Log($"Unlocked class: {data.furnitureType}");

		// Activate UI / Enable drop slots etc.
	}

	private void UnlockStyle(FurnitureStyle style) {
		Debug.Log($"Unlocked style: {style}");
		// Set style active
	}

	private void UnlockInterface(FurnitureType type) {
		Debug.Log($"Unlocked interface: {type}");
		// Enable drag & drop etc.
	}
}

