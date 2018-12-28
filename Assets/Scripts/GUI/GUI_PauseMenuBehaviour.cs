using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GUI_PauseMenuBehaviour : MonoBehaviour {

	public void Options_Click() {
		GUIManager.Instance.ShowWindow("Options");
	}

	public void LoadGame_Click() {
		Debug.Log("Load Game is not yet implemented.");
	}

	public void SaveContinue_Click() {
		Debug.Log("Save and Continue is not yet implemented.");
	}

	public void SaveExit_Click() {
		Debug.Log("Save and Exit is not implemented.");
	}

	public void Unpause_Click() {
		GUIManager.Instance.CloseWindow();
	}
}
