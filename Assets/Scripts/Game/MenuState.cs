using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState {


	public MenuState() {
		Transitions = "";
		TimeScale = 0.0f;
	}

	public override void Enable() {		
        Time.timeScale = TimeScale;	
	}

	public override void Disable() {

	}

}
