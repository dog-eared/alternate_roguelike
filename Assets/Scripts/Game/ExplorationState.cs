using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationState : GameState {

	public ExplorationState() {
		Transitions = "TargetingState MenuState DialogState";
		TimeScale = 1.0f;
	}

	public override void Enable() {
		Time.timeScale = TimeScale;
	}

	public override void Disable() {
		//Hide Exploration related GUI
	}

}
