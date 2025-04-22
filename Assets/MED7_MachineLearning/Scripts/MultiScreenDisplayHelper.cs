using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScreenDisplayHelper : MonoBehaviour {

	void Start() {
		ActivateDisplays();
	}

	// Check if additional displays are available and activate each.
	void ActivateDisplays() {
		Debug.Log("displays connected: " + Display.displays.Length);
		for (int i = 1; i < Display.displays.Length; i++) {
			Display.displays[i].Activate();
		}
	}
}
