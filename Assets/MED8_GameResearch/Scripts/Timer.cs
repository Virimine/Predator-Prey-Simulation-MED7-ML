using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

	[SerializeField] TextMeshProUGUI displayTMP;
	[SerializeField] float totalTime = 60f;

	float remainingTime;
	bool running;
	System.Action onTimerFinished;

	void Start() => UpdateTimerDisplay(totalTime);

	void Update() {
		if (!running) { return; }

		UpdateTimerDisplay(remainingTime);

		remainingTime -= Time.deltaTime;
		if (remainingTime <= 0f) {
			running = false;
			onTimerFinished?.Invoke();
		}
	}

	public void StartTimer(System.Action callback) {
		remainingTime = totalTime;
		onTimerFinished = callback;
		running = true;
	}

	public void UpdateTimerDisplay(float timeInSeconds) {
		int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
		int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
		displayTMP.text = $"time: {minutes:00}:{seconds:00}";
	}
}
