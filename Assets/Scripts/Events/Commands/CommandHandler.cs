using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //remove later

public class CommandHandler : MonoBehaviour {

	public GameObject mover;
	public GameObject mover2;
	public Text debugOutput;

	private Queue<Command> commands = new Queue<Command>();
	private IEnumerator callActions;

	void Update () {
		if (Input.GetKeyDown("d")) {
			commands.Enqueue(new MoveCommand(mover, new Vector2Int(1, 0)));
			debugOutput.text += "\n Moved mover1 r";
		}

		if (Input.GetKeyDown("a")) {
			commands.Enqueue(new MoveCommand(mover, new Vector2Int(-1, 0)));
			debugOutput.text += "\n Moved mover1 le";
		}

		if (Input.GetKeyDown("e")) {
			commands.Enqueue(new MoveCommand(mover2, new Vector2Int(1, 0)));
			debugOutput.text += "\n Moved mover2 r";
		}

		if (Input.GetKeyDown("q")) {
			commands.Enqueue(new MoveCommand(mover2, new Vector2Int(-1, 0)));
			debugOutput.text += "\n Moved mover2 le";
		}

		if (Input.GetKeyDown("s")) {
			if (commands.Count > 0 && callActions == null) {
				callActions = CallActions();
				StartCoroutine(callActions);
			}
		}

		if (Input.GetKeyDown("p")) {
			debugOutput.text = "";
		}
	}

	private IEnumerator CallActions() {
		while (commands.Count > 0) {
			List<Command> commandBlock = GetCommandBlock();

			float longestAnimation = 0f;
			foreach (Command c in commandBlock) {
				c.Execute();
				longestAnimation = (longestAnimation > c.Length ? longestAnimation : c.Length);
			}

			yield return new WaitForSeconds(longestAnimation);
		}

		StopCoroutine(callActions);
		callActions = null;
	}

	private List<Command> GetCommandBlock() {

		List<Command> commandBlock = new List<Command>();
		if (commands.Peek().GetType() != typeof(MoveCommand)) {
			commandBlock.Add(commands.Dequeue());
			return commandBlock;
		}

		List<GameObject> alreadyMoved = new List<GameObject>();
		while (commands.Count > 0
				&& commands.Peek().GetType() == typeof(MoveCommand)
				&& !alreadyMoved.Contains(commands.Peek().Mover)) {
			alreadyMoved.Add(commands.Peek().Mover);
			commandBlock.Add(commands.Dequeue());
		}

		return commandBlock;
	}
}
