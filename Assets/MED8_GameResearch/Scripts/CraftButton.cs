using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour {

	[SerializeField] Button button;

	public event Action OnCraftButtonPressed;

	void Start() => button.onClick.AddListener(() => OnCraftButtonPressed?.Invoke());

}
