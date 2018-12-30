using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCombatData : MonoBehaviour {

	public Vector2Int mapLocation;

	private void Awake() {
		CreatureManager.AddCreature(this);
	}

	public void UpdateLocation(Vector3 location) {
		mapLocation = new Vector2Int((int)location.x, (int)location.y);
	}


}
