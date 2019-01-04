using UnityEngine;

public class PauseCommand : Command {
	/**
	 * PAUSE COMMAND
	 * Purpose: Issued when creature is unable to act but isn't otherwise impeded, or
	 * when the player chooses to do nothing.
	 */

	public PauseCommand(CreatureCombatData source, string animType = "None") {
		Source = source;
		Length = 0.08f;
		AnimationType = animType;
		Groupable = true;
	}

	public override bool Execute() {
		Debug.Log(Source.gameObject.name + " pauses.");
		return true;
	}

	public override void Cleanup() {
		//Unused.
	}
}
