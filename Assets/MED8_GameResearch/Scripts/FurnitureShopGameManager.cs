using Mono.Cecil.Cil;
using UnityEngine;

public enum GameState {
	Opening,
	FurnitureAssembly,
	Closing
}

public class FurnitureShopGameManager : MonoBehaviour {

	[SerializeField] Timer timer;

	public Canvas mainCanvas { get; private set; }

	public OrderManager orderManager { get; private set; }
	public DialogueManager dialogueManager { get; private set; }
	public InterfaceManager interfaceManager { get; private set; }

	public GameState currentState { get; private set; }

	public static FurnitureShopGameManager instance { get; private set; }

	/// Loading Screen: Suggestive Screen, implies what comes next
	// Software Design Patterns / inspired by Dive Into DESIGN PATTERNS by Alexander Shvets

	// Chapter I: Creational Design Patterns Ooooo

	// ABSTRACT FACTORY
	//Abstract Factory is a creational design pattern that lets you
	//produce families of related objects without specifying their
	//concrete classes.

	/// COMPONENTS
	// upgrade btns
	// tool tips

	/// CODE REFACTOR
	// make event manager and event driven architecture
	// split data, logic and view (MVVM)


	void Awake() {
		if (instance != null && instance != this) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

		orderManager = GetComponentInChildren<OrderManager>();
		dialogueManager = GetComponentInChildren<DialogueManager>();
		interfaceManager = GetComponentInChildren<InterfaceManager>();

		mainCanvas = GetComponentInChildren<Canvas>();
	}

	void Start() => SetState(GameState.Opening);

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) { timer.AddDebugTime(); }
		if (Input.GetKeyDown(KeyCode.Q)) { CoinsHandler.GainCoins(50); }
	}

	public void SetState(GameState newState) {
		currentState = newState;
		Debug.Log($"Switched to state: {newState}");

		switch (newState) {
			case GameState.Opening:
				HandleOpening();
				break;

			case GameState.FurnitureAssembly:
				HandleAssembly();
				break;

			case GameState.Closing:
				HandleClosing();
				break;
		}
	}


	void HandleOpening() {
		// play title screen
		dialogueManager.EnterDialogueMode();
	}
	void HandleAssembly() {
		orderManager.TakeOrder();
		timer.StartTimer(OnAssemblyTimeFinished);
	}

	void HandleClosing() => interfaceManager.ShowUpgradeScreen();

	void OnAssemblyTimeFinished() => SetState(GameState.Closing);

	public void ProceedToNextDay() => SetState(GameState.Opening);
}
