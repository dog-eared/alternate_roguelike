using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CommandHelper : MonoBehaviour {

	private const int layerMask = 256;
	private const float forceCollisionShrink = 0.06f; //Used to ensure pass check is inside bounds


	public static bool CheckPassableAt(Vector2 location, int size = 1) {

		float adjustedSize = size - forceCollisionShrink;

		if (Physics2D.OverlapBox(location, new Vector2(adjustedSize, adjustedSize), 0, layerMask) == null) {
			return true;
		}

		return false;
	}

}
