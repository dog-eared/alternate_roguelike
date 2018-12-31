using UnityEngine;

public class PauseCommand : Command {

	public override float Length {get;}
	public override CreatureCombatData Source {get;}
	public override string AnimationType {get;}

	public PauseCommand(CreatureCombatData source, string animType = "None") {
		Source = source;
		Length = 0.08f;
		AnimationType = animType;
	}

	public override bool Execute() {
		Debug.Log(Source.gameObject.name + " pauses.");
		return true;
	}

	public override void Cleanup() {
		//Unused.
	}
}
