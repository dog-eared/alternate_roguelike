using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	public static GameStateManager Instance {get; private set;}

	public GameState current;
	public static Stack<GameState> stateStack = new Stack<GameState>();

	public float gameSpeed = 1.0f;

	void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}
		GUIManager.MenuChanged.AddListener(CheckGUIState);
		
		//CreatureManager.Instance.Invoke("NewRound", 0.05f);

		AddState(CreateState("exploration"));
	}

	private void AddState(GameState state) {

		if (stateStack.Count > 0) {
			stateStack.Peek().Disable();
		}
	
		stateStack.Push(state);
		stateStack.Peek().Enable();
	}

	private void RemoveState() {
		if (stateStack.Count > 1) {
			stateStack.Pop().Disable();
			stateStack.Peek().Enable();
		}
	}

	private void CheckGUIState() {
		if (stateStack.Peek().GetType() == typeof(MenuState) && GUIManager.visibleWindows.Count == 0) {
			RemoveState();
		} else if (stateStack.Peek().GetType() != typeof(MenuState) && GUIManager.visibleWindows.Count > 0) {
			AddState(CreateState("menu"));
		}
	}

	private GameState CreateState(string newState) {
		switch (newState.ToLower()) {
			case "exploration":
				return new ExplorationState();
			case "menu":
				return new MenuState();
			case "combat":
				return new CombatState();
			default:
				Debug.Log("CreateState failed: invalid state name!");
				return null;
		}
	}

	/* COMBAT */
	public static void CallActions() {
		IEnumerator go = CallActionsIEnumerator();
		Instance.StartCoroutine(go);
	}

	private static IEnumerator CallActionsIEnumerator() {
		if (CommandHandler.Execute()) {
			PlayerInputManager.acceptingInput = false;
			
			yield return new WaitUntil(() => CommandHandler.Instance.DoneAnimating == true);

			CreatureManager.NewRound();
			PlayerInputManager.acceptingInput = true;
		}
	}
}
