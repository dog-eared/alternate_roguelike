using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SimplePath : AI_Base {

	/* 	AI: SimplePath
	*	Walks dumbly between all given waypoints. Appropriate for both enemy and neutral
	*	characters.
	*/

	public int patrolCounter = 0;
	public Vector2Int[] steps;

	public override Command NextStep() {

		if (data == null) {
			//If the GetComponent in Awake() hasn't fired for some reason, this should buy some time.
			return Command.New("pause", GetComponent<CreatureCombatData>());
		}


		if (steps.Length == 0) {
			return Command.New("pause", data);
		}

		if (data.MapLocation == steps[patrolCounter] ) {
			patrolCounter++;
		}

		if (patrolCounter >= steps.Length) {
			patrolCounter = 0;
		}

		

		return Command.New("move", data, NextStepDirection());

	}

	public Vector2Int NextStepDirection() {
		Vector2Int loc = data.MapLocation;

		int x = (int)Mathf.Clamp(steps[patrolCounter].x - loc.x, -1f, 1);
		int y = (int)Mathf.Clamp(steps[patrolCounter].y - loc.y, -1f, 1);
		return new Vector2Int(x, y);
	}

	public Command Attack(Vector2Int direction) {
		try {
			
			return Command.New("melee", data, direction);
		} catch {
			return Command.New("pause", data);
		}
	}

}
