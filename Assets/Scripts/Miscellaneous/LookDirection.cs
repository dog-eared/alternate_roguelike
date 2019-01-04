using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection {

	/* LOOKDIRECTION (Helper class)
	 * 
	 * Used to get proper angle for 3D models
	 */

	private const float t = 45; //Tilt
	private const float ht = 22.5f; //Half Tilt

	public static Vector3 Look(Vector2Int direction) {
		switch (direction.x) {
			case (-1):
				switch (direction.y) {
					case (-1):
						return new Vector3(ht, 225, ht);
					case (0):
					 	return new Vector3(0, 270, t);
					default:
						return new Vector3(-ht, 315, ht);
				}
			case (0):
				switch (direction.y) {
					case (-1):
						return new Vector3(t, 180, 0);
					case (0):
						return new Vector3(-t, 0, 0);
					default:
						return new Vector3(-t, 0, 0);
				}
			case (1):
				switch (direction.y) {
					case (-1):
						return new Vector3(ht, 135, -ht);
					case (0):
						return new Vector3(0, 90, -ht);
					default:
						return new Vector3(-ht, 45, -ht);
				}
			default:
				return new Vector3(-t, 0, 0);
		}
	}

}
