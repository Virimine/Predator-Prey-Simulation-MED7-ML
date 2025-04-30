using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Furniture", menuName = "ScriptableObjects/Furniture")]
public class FurnitureData : ScriptableObject {
	public FurnitureType type;
	public List<FurnitureFunctionality> functionalities;
	//public FurnitureStyle style;
}

public enum FurnitureType {
	Chair,
	Sofa,
	Table
}

public enum FurnitureFunctionality {
	SitOn,
	HasLegs,
	FancyUncomfortable,
}

public enum FurnitureStyle {
	Plain,
	Modern,
	Scandi,
	Boho
}
