using UnityEngine;

public abstract class Command {

	public abstract float Length {get;}
	public virtual CreatureCombatData Source {get;}
	public virtual string AnimationType {get;}

	public abstract bool Execute();
	public abstract void Cleanup();



	/* NEW COMMAND METHODS: */

	public static Command New(string cmdType, CreatureCombatData source, Vector2Int direction) {

		//Check that cmdType is OK and deplete appropriate energy
		try {
			source.Deplete(GameSettings.actionCosts[cmdType]);
		} catch {
			Debug.Log("Invalid cmdType!");
			return null;
		}

		switch (cmdType) {
			case "move":
				return new MoveCommand(source, direction);
			default:
				return null;
		}
	}

	public static Command New(string cmdType, CreatureCombatData source) {
		switch (cmdType) {
			case "pause":
				return new PauseCommand(source);
			default:
				return null;
		}
	}
}
