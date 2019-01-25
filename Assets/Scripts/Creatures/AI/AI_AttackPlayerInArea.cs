using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackPlayerInArea : AI_Base {

	/* 	HOSTILE AI: Attack Player in Area
	*	Walks dumbly within a square boundary. If a character is in bounds, moves to attack them.
	*/

	public CreatureCombatData player;
	private Vector2Int playerLoc;
	public Vector2Int topLeftBound;
	public Vector2Int bottomRightBound;
	public bool playerInBounds;

	private int maxStepCheck = 3;

	public override Command NextStep() {
		return Command.New("move", data, NextStepDirection());
	}

	public Vector2Int NextStepDirection() {
		
		playerInBounds = CheckTargetInBounds();

		if (playerInBounds) {
			Vector2Int loc = data.MapLocation;

			int x = (int)Mathf.Clamp(playerLoc.x - loc.x, -1f, 1);
			int y = (int)Mathf.Clamp(playerLoc.y - loc.y, -1f, 1);

			return new Vector2Int(x, y);

		} else {
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

	private bool CheckTargetInBounds() {
		if (player != null) {
			playerLoc = player.MapLocation;

			if (playerLoc.x > topLeftBound.x && playerLoc.x < bottomRightBound.x
			&&	playerLoc.y < topLeftBound.y && playerLoc.y > bottomRightBound.y) {
				Debug.Log("playerLoc is in bounds");
				return true;
			}

		} else {
			try {
				player = PlayerInputManager.Player;
			} catch {
				//nope!
			}
		}
		return false;
	}

}
