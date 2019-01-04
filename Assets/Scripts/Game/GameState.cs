using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState {

	/* Will be set in constructor. */
	public virtual string Transitions {get; protected set;}
	public virtual float TimeScale {get; protected set;}

	public abstract void Enable();
	public abstract void Disable();

}
