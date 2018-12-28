using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	public static GameStateManager Instance {get; private set;}
	public GameState current;

	void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}

		current = GameState.Waiting;
	}

	private void Update() {
		switch (current) {
			case GameState.Waiting:

				break;
			case GameState.Animating:

				break;
			case GameState.InMenu:
				break;
			default:
				break;
		}



		if (GUIManager.visibleWindows.Count > 0) {
			current = GameState.InMenu;
		} else if (GUIManager.visibleWindows.Count <= 0) {
			current = GameState.Waiting;
		}

	}
}

public enum GameState {
	Waiting,
	Animating,
	InMenu
}
