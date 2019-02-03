using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : GameState {

	public CombatState() {
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