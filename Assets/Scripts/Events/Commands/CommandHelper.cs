using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CommandHelper : MonoBehaviour {

	private const int worldMask = 256;
	private const int creatureMask = 512;
	private const float forceCollisionShrink = 0.06f; //Used to ensure pass check is inside bounds

	public static bool CheckPassableAt(Vector2 location, int size = 1) {

		float adjustedSize = size - forceCollisionShrink;

		if (Physics2D.OverlapBox(location, new Vector2(adjustedSize, adjustedSize), 0, worldMask) == null) {
			return true;
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
