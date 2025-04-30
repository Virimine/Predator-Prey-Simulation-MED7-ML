using UnityEngine;
using UnityEngine.UI;

public class InterfaceDrag : FurnitureDrag {

	[SerializeField] Button removeFunctionality;

	protected override void Awake() {
		base.Awake();
		removeFunctionality.onClick.AddListener(() => ClearStoredFunctionalities());
	}

	void ClearStoredFunctionalities() {
		Debug.Log("Cleared stored functionalities.");
		// Clean up UI and logic
	}
}