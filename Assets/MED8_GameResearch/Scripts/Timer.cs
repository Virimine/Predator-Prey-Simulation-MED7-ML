using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

	[SerializeField] TextMeshProUGUI displayTMP;
	[SerializeField] Animator animator;
	[SerializeField] float totalTime = 60f;

	string triggerName = "StartAnim";
	float triggerTime = 10f;
	 bool hasTriggered;

	float remainingTime;
	bool running;
	System.Action onTimerFinished;

	void Start() => UpdateTimerDisplay(totalTime);

	void Update() {
		if (!running) { return; }

		UpdateTimerDisplay(remainingTime);

		remainingTime -= Time.deltaTime;

		PlayLowTimeAnimation();

		if (remainingTime <= 0f) {
			onTimerFinished?.Invoke();
			ResetTimer();
		}
	}

	public float AddDebugTime() => totalTime += 3000;

	public void StartTimer(System.Action callback) {

		running = true;

		remainingTime = totalTime;
		onTimerFinished = callback;
	}

	public void UpdateTimerDisplay(float timeInSeconds) {
		int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
		int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
		displayTMP.text = $"time: {minutes:00}:{seconds:00}";
	}

	void PlayLowTimeAnimation(){

		if (!hasTriggered && remainingTime <= triggerTime) {

			animator.SetTrigger(triggerName);
			hasTriggered = true;
		}
	}

	void ResetTimer(){
		hasTriggered = false;
		running = false;
		animator.Play("Default");
	}
}
