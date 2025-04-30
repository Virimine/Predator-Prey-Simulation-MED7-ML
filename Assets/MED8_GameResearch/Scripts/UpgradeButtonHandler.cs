using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] Button upgradeButton;
	[SerializeField] TextMeshProUGUI costText;
	[SerializeField] TextMeshProUGUI descriptionText;
	[Space]
	[SerializeField] UpgradeData upgradeData;
	[SerializeField] GameObject objectEnable;
	[SerializeField] GameObject objectDisable;


	private void Start() {
		costText.text = $"Cost: {upgradeData.cost}";

		// if upgrade == furniture then this: Else, use style

		upgradeButton.onClick.AddListener(AttemptUpgrade);
	}

	void AttemptUpgrade() {
		if (CoinsHandler.coins < upgradeData.cost) {
			Debug.Log("Not enough coins!");
			return;
		}

		CoinsHandler.SpendCoins(upgradeData.cost);
		UpgradeEvents.OnUpgradePurchased?.Invoke(upgradeData);

		if (objectDisable != null) { objectDisable.SetActive(false); }
		if (objectEnable != null) { objectEnable.SetActive(true); }

		upgradeButton.interactable = false;
	}

	public void OnPointerEnter(PointerEventData eventData) {

		if (upgradeData.description.Contains("[furniture]")) {
			descriptionText.text = DialogueFormatter.FormatText(upgradeData.description, upgradeData.furnitureType.ToString(), "[furniture]");
			return;
		}

			descriptionText.text = DialogueFormatter.FormatText(upgradeData.description, upgradeData.styleType.ToString(), "[style]");
	}

	public void OnPointerExit(PointerEventData eventData) {

		descriptionText.text = "";
	}
}
