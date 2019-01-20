using UnityEngine;

public abstract class Command {
	/**
	 * COMMAND (abstract, non-monobehaviour)
	 * Purpose: Base for issued commands. Contains relevant properties and enforces implementation of Execute() and
	 * Cleanup(). Also contains methods to constrain creation of new Commands
	 */

	public virtual CreatureCombatData Source {get; protected set;}
	public virtual float Length {get; protected set;}
	public virtual string AnimationType {get; protected set;}
	public virtual bool Groupable {get; protected set;}

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
			case "melee":
				return new MeleeCommand(source, direction);
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
