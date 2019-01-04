using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CommandHelper : MonoBehaviour {
	/**
	 * COMMAND HELPER
	 * Purpose: Extra command related methods that don't fit anywhere else.
	 */

	private const int worldMask = 256;
	private const int creatureMask = 512;
	private const int allMask = 768;

	private const float forceCollisionShrink = 0.06f; //Used to ensure pass check is inside bounds

	public static bool CheckPassableAt(Vector2 location, int size = 1) {

		float adjustedSize = size - forceCollisionShrink;

		if (Physics2D.OverlapBox(location, new Vector2(adjustedSize, adjustedSize), 0, worldMask) == null
		&& CheckCreatureAt(location) == false) {
			return true;
		}
		return false;
	}

	public static bool CheckCreatureAt(Vector2 location) {
		for (int i = 0; i < CreatureManager.Instance.creatures.Count; i++) {
			Vector2Int check = CreatureManager.Instance.creatures[i].MapLocation;
			if ( (location.x == check.x) && (location.y == check.y) ) {
				return true;
			}
		}
		return false;
	}

	public static CreatureCombatData GetCreatureAt(Vector2 location) {
		try {
			return Physics2D.OverlapBox(location, new Vector2(0.9f, 0.9f), 0, creatureMask).GetComponent<CreatureCombatData>();
		} catch {
			return null;
		}
	}


}
