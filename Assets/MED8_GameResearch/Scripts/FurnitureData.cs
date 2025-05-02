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
	OverthinkerThrone,
	SilentSupport,
	LaundryButler,

	NapEngine,
	CrisisCouch,
	FelineCertified,

	SnackStation,
	DancersRegret,
	ToeTarget,

	ComfyChaos,
	TextileParty,

	DesignerDiscomfort,
	JudgyElegance,

	WoodEverywhere,
	ColdComfort,
}

public enum FurnitureStyle {
	Plain,
	Modern,
	Scandi,
	Boho
}
