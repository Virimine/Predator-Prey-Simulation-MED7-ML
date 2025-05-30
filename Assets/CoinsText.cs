using TMPro;
using UnityEngine;


public class CoinsText : MonoBehaviour {
	TextMeshProUGUI textMesh;

	void Awake() => textMesh = GetComponentInChildren<TextMeshProUGUI>();

	void Start() {
		CoinsHandler.OnCoinsChanged += UpdateCoinsText;
		UpdateCoinsText(CoinsHandler.coins);
	}

	public void UpdateCoinsText(int newAmount) => textMesh.text = $"coins: {newAmount}";

}
