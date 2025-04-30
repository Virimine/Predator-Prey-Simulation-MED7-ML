using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
	[SerializeField] List<StoryDialogue> dialogues;
	[SerializeField] TextMeshProUGUI dialogueText;
	[SerializeField] float typingSpeed;

	Coroutine displayLineCoroutine;

	StoryDialogue dialogue;
	int dialogueIndex;
	int currentLineIndex;
	bool isRequest;

	public event Action OnDialogueComplete;

	public bool isPlaying { get; private set; }

	bool canProceed;
	public bool CanProceed {

		get => canProceed;
		set {
			canProceed = value;
			FurnitureShopGameManager.instance.interfaceManager.ShowProceedButton(canProceed);
		}
	}

	public void DisplayRequest(CustomerRequest customerRequest) {

		currentLineIndex = 0; // if we remove this we get an error, idk why rn
		isRequest = true;

		var furnitureData = customerRequest.furniture;
		var furnitureType = furnitureData.type.ToString();
		List<string> furnitureFuncs = new List<string>();

		foreach (var func in furnitureData.functionalities) {
			furnitureFuncs.Add(func.ToString());
		}

		var customerRequestLine = DialogueFormatter.FormatText(customerRequest.lines[currentLineIndex], furnitureType, furnitureFuncs);
		StartCoroutine(DisplayLine(customerRequestLine));
	}

	public void EnterDialogueMode() {

		if (dialogueIndex >= dialogues.Count) { return; }

		dialogue = dialogues[dialogueIndex];
		isPlaying = true;
		isRequest = false;

		ContinueDialogue();
	}

	public void ContinueDialogue() {

		if (displayLineCoroutine != null) {
			StopCoroutine(displayLineCoroutine);
		}

		var hasLinesLeft = currentLineIndex < dialogue.lines.Count;
		if (hasLinesLeft) {
			displayLineCoroutine = StartCoroutine(DisplayLine(dialogue.lines[currentLineIndex]));
		}

		if (currentLineIndex == dialogue.lines.Count - 1) {
			ExitDialogueMode();
		}
	}

	void ExitDialogueMode() {

		isPlaying = false;
		currentLineIndex = -1;
		dialogueIndex++;

		OnDialogueComplete?.Invoke();
	}

	IEnumerator DisplayLine(string line) {

		CanProceed = false;
		dialogueText.text = "";

		foreach (var letter in line.ToCharArray()) {
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

		currentLineIndex++;
		yield return new WaitForSeconds(0.1f);

		CanProceed = !isRequest;

		yield return null;
	}
}
