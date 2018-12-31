using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationState : GameState {

	public ExplorationState() {
		Transitions = "TargetingState MenuState DialogState";
	}

	public override void Enable() {
		//Show Exploration related GUI
	}

	public override void Disable() {
		//Hide Exploration related GUI
	}

}
