using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionPanel : MonoBehaviour {
	[SerializeField] TextMeshProUGUI itemNameTMP;
	[SerializeField] TextMeshProUGUI descriptionTMP;
	[SerializeField] TextMeshProUGUI rarityTMP;
	[SerializeField] TextMeshProUGUI specialPowerTMP;
	[SerializeField] TextMeshProUGUI specialPowerDescriptionTMP;
	[SerializeField] TextMeshProUGUI attackTMP;

	public void UpdateTexts(EquipmentData item) {
		itemNameTMP.text = item.itemName;
		rarityTMP.text = item.rarity.ToString().ToUpper();
		descriptionTMP.text = item.description;
		specialPowerTMP.text = item.specialPower;
		specialPowerDescriptionTMP.text = item.specialPowerDescription;
		attackTMP.text = $"{item.attack} \n {item.magicAttack} \n {item.speed}";
	}
}
