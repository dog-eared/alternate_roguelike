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

	public void NextStep() {

	}

	public Vector2 NextStepDirection() {
		for (int i = 0; i < maxStepCheck; i++) {
			//Apply random value to location for attempted move
			Vector2 move = RandomDirection.Step();
			Vector2 locationCheck = transform.position + (Vector3)move;

			//Check move is ok; if so, return valid move
			if (locationCheck.x > topLeftBound.x && locationCheck.x < bottomRightBound.x
			&&	locationCheck.y < topLeftBound.y && locationCheck.y > bottomRightBound.y) {
				return move;
			}
		}

		//Loop did not work; return zero
		return Vector2.zero;
	}
}
