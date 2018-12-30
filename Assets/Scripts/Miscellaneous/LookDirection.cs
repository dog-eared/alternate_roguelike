using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection {

	public static Vector3 Look(Vector2Int direction, Vector3 init = new Vector3()) {
		switch (direction.x) {
			case (-1):
				switch (direction.y) {
					case (-1):
						return new Vector3(init.x, 225, init.z);
					case (0):
					 	return new Vector3(init.x, 270, init.z);
					default:
						return new Vector3(init.x, 315, init.z);
				}
			case (0):
				switch (direction.y) {
					case (-1):
						return new Vector3(init.x, 180, init.z);
					case (0):
						return new Vector3(init.x, 0, init.z);
					default:
						return new Vector3(init.x, 0, init.z);
				}
			case (1):
				switch (direction.y) {
					case (-1):
						return new Vector3(init.x, 135, init.z);
					case (0):
						return new Vector3(init.x, 90, init.z);
					default:
						return new Vector3(init.x, 45, init.z);
				}
			default:
				return init;
		}
	}

}
