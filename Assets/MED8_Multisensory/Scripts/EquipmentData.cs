using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	public enum Rarity { common, uncommon, rare, legendary }

[CreateAssetMenu(fileName = "EquipmentData", menuName = "ScriptableObjects/EquipmentData")]
public class EquipmentData : ScriptableObject {


	public string itemName;
	public Sprite sprite;
	public Rarity rarity;
	public string description;
	[Space]
	public string specialPower;
	public string specialPowerDescription;
	[Space]
	public int attack;
	public int magicAttack;
	public int speed;
}
