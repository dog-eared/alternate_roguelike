using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public static GUIManager Instance {get; private set;} //Enforce singleton

	public List<GameObject> guiElements = new List<GameObject>();
	public static Stack<GameObject> visibleWindows = new Stack<GameObject>();

	public static Dictionary<string, int> options = new Dictionary<string, int>() {
		{"sfxVolume", 		9},
		{"musicVolume", 	9},
		{"hardMode", 		0},
		{"violenceMode",	1}
	};

	/* PRIVATE METHODS */

	private void Awake() {
		//Enforce singleton pattern
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log("ERROR: There should only be one GUIManager");
			Destroy(this);
		}

		SetOptions();

		foreach (Transform child in transform) {
			guiElements.Add(child.gameObject);
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (visibleWindows.Count > 0) {
				CloseWindow();
			} else if (GameStateManager.Instance.current != GameState.Animating) {
				ShowWindow("Pause");
			}
		}

		if (Input.GetKeyDown("1")) {
			ShowWindow(0);
		}

		if (Input.GetKeyDown("2")) {
			ShowWindow(1);
		}

		if (Input.GetKeyDown("3")) {
			ShowWindow(2);
		}
	}

	/* GENERIC PUBLIC METHODS */

	public void ShowWindow(int windowIndex) {
		if (visibleWindows.Count > 0) {
			visibleWindows.Peek().SetActive(false);
		}

		visibleWindows.Push(guiElements[windowIndex]);
		visibleWindows.Peek().SetActive(true);
	}

	public void ShowWindow(string windowName) {
		for (int i = 0; i < guiElements.Count; i++) {
			if (guiElements[i].name.Contains(windowName)) {
				ShowWindow(i);
				return;
			}
		}
	}

	public static void SetOptions() {
		options["sfxVolume"] = PlayerPrefs.GetInt("sfxVolume", 7);
		options["musicVolume"] = PlayerPrefs.GetInt("musicVolume", 7);
		options["hardMode"] = PlayerPrefs.GetInt("hardMode", 0);
		options["violenceMode"] = PlayerPrefs.GetInt("violenceMode", 1);
	}

	public void CloseWindow() {
		if (visibleWindows.Count > 0) {
			visibleWindows.Peek().SetActive(false);
			visibleWindows.Pop();
		}

		//Second check, incase we're still over zero
		if (visibleWindows.Count > 0) {
			visibleWindows.Peek().SetActive(true);
		}
	}

}
