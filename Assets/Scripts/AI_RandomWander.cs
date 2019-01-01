using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RandomWander : MonoBehaviour {

	private CreatureCombatData data;
	private int maxStepCheck = 3;

	public Vector2Int topLeftBound;
	public Vector2Int bottomRightBound;

	void Awake() {
		if (data == null) {
			data = GetComponent<CreatureCombatData>();
		}
	}

	public Command NextStep() {
		Vector2Int direction = NextStepDirection();
		if (direction == Vector2.zero) {
			return Command.New("pause", data);
		} else {
			return Command.New("move", data, direction);
		}
	}

	public Vector2Int NextStepDirection() {
		for (int i = 0; i < maxStepCheck; i++) {
			//Apply random value to location for attempted move
			Vector2Int move = RandomDirection.Step();
			Vector2Int locationCheck = data.MapLocation + move;

			//Check move is ok; if so, return valid move
			if (locationCheck.x > topLeftBound.x && locationCheck.x < bottomRightBound.x
			&&	locationCheck.y < topLeftBound.y && locationCheck.y > bottomRightBound.y) {
				return move;
			}
		}

		//Loop did not work; return zero
		return Vector2Int.zero;
	}
}
