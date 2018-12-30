using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //remove later

public class CommandHandler : MonoBehaviour {

	public static CommandHandler Instance {get; private set;}

	public CreatureCombatData mover;
	public CreatureCombatData mover2;
	public Text debugOutput;

	private static Queue<Command> commands = new Queue<Command>();
	private IEnumerator callActions;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log("ERROR: There should only be one CommandHandler");
			Destroy(this);
		}
	}

	public bool Execute() {
		if (commands.Count > 0 && callActions == null) {
			callActions = CallActions();
			StartCoroutine(callActions);
			return true;
		}

		Debug.Log("CommandHandler was unable to execute: "
					+ commands.Count + " commands and callActions exists = "
					+ (callActions == null));
		return false;
	}

	private IEnumerator CallActions() {
		while (commands.Count > 0) {
			List<Command> commandBlock = GetCommandBlock();

			float longestAnimation = 0f;
			foreach (Command c in commandBlock) {
				 if (c.Execute()) {
					 longestAnimation = (longestAnimation > c.Length ? longestAnimation : c.Length);
				 }
			}

			yield return new WaitForSeconds(longestAnimation);
		}

		StopCoroutine(callActions);
		callActions = null;
	}

	public bool DoneAnimating() {
		return callActions == null;
	}

	public void EnqueueCommand(Command c) {
		commands.Enqueue(c);
	}

	private List<Command> GetCommandBlock() {

		List<Command> commandBlock = new List<Command>();
		if (commands.Peek().GetType() != typeof(MoveCommand)) {
			commandBlock.Add(commands.Dequeue());
			return commandBlock;
		}

		List<CreatureCombatData> alreadyMoved = new List<CreatureCombatData>();
		while (commands.Count > 0
				&& commands.Peek().GetType() == typeof(MoveCommand)
				&& !alreadyMoved.Contains(commands.Peek().Source)) {
			alreadyMoved.Add(commands.Peek().Source);
			commandBlock.Add(commands.Dequeue());
		}

		return commandBlock;
	}
}
