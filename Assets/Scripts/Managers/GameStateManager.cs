using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	public static GameStateManager Instance {get; private set;}

	public GameState current;
	public Stack<GameState> stateStack = new Stack<GameState>();

	public float gameSpeed = 1.0f;

	void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}

		CreatureManager.NewRound();
	}

	private void AddState(GameState state) {
		stateStack.Peek().Disable();
		stateStack.Push(state);
		stateStack.Peek().Enable();
	}

	private GameState CreateState(string newState) {
		if (newState.ToLower() == "exploration") {
			return new ExplorationState();
		} else {
			Debug.Log("CreateState failed: invalid state name!");
			return null;
		}
	}

	public static void CallActions() {
		IEnumerator go = CallActionsIEnumerator();
		Instance.StartCoroutine(go);
	}

	private static IEnumerator CallActionsIEnumerator() {
		if (CommandHandler.Execute()) {
			PlayerInputManager.acceptingInput = false;
			yield return new WaitUntil(() => CommandHandler.Instance.DoneAnimating() == true);

			CreatureManager.NewRound();
			PlayerInputManager.acceptingInput = true;
		}
	}
}
