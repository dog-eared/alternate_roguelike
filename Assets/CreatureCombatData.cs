using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCombatData : MonoBehaviour {

	public Vector2Int mapLocation;

	public int energy = 100;
	public int maxEnergy = 125;
	public int energyRecharge = 50;

	public AI_RandomWander AI {get; private set;}

	public bool Active {
		get {
			if (AI != null) {
				return (energy >= 0);
			}
			return false;
		}
	}

	private void Awake() {
		CreatureManager.AddCreature(this);

		try {
			AI = GetComponent<AI_RandomWander>();
		} catch {
			//This is a player
		}
	}

	public void UpdateLocation(Vector3 location) {
		mapLocation = new Vector2Int((int)location.x, (int)location.y);
	}

	public void Recharge() {
		//If allowed, recharge energy
		energy = Mathf.Min(maxEnergy, energy + energyRecharge);
	}

	public bool Deplete(int amount) {
		if (amount <= energy) {
			energy -= amount;
			return true;
		} else {
			return false;
		}
	}


}
