using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour {
	/**
	 * COMMAND HANDLER
	 * Purpose: Call commands sequentially and animate them (grouped together if
	 * appropriate);
	 */

	public static CommandHandler Instance {get; private set;}

	private static Queue<Command> commands = new Queue<Command>();
	private IEnumerator callActions;

	public bool DoneAnimating {
		get {
			return callActions == null;
		}
	}

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Debug.Log("ERROR: There should only be one CommandHandler");
			Destroy(this);
		}
	}


	/* PUBLIC METHODS */

	public static bool Execute() {
		if (commands.Count > 0 && Instance.callActions == null) {
			Instance.callActions = Instance.CallActions();
			Instance.StartCoroutine(Instance.callActions);
			return true;
		}

		Debug.Log("CommandHandler was unable to execute: "
					+ commands.Count + " commands and callActions exists = "
					+ (Instance.callActions == null));
		return false;
	}

	public void EnqueueCommand(Command c) {
		//Adds a new command to the queue.
		commands.Enqueue(c);
	}


	/* PRIVATE METHODS */

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

	private List<Command> GetCommandBlock() {
		//Gets a group of commands

		List<Command> commandBlock = new List<Command>();

		if (!commands.Peek().Groupable) {
			commandBlock.Add(commands.Dequeue());
			return commandBlock;
		}

		List<CreatureCombatData> alreadyMoved = new List<CreatureCombatData>();
		while (commands.Count > 0
				&& commands.Peek().Groupable
				&& !alreadyMoved.Contains(commands.Peek().Source)) {
			alreadyMoved.Add(commands.Peek().Source);
			commandBlock.Add(commands.Dequeue());
		}

		return commandBlock;
	}
}
