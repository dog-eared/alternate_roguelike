using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCombatData : MonoBehaviour {
	/**
	 * CREATURE COMBAT DATA
	 * Purpose: Contain all individualized combat data.
	 */

	public Vector2Int MapLocation {get; private set;}

	[Header("Information")]
	public string displayName = "Creature";
	public int factionID = 0; //Neutral

	[Header("Stats (Numeric)")]
	public int health;
	public int attackPower;
	public bool isDead = false;

	/* Energy */
	public int energy = 100;
	public int maxEnergy = 125;
	public int energyRecharge = 50;

	public AI_Base AI {get; private set;}
	public CreatureAnimationHandler AnimHandler {get; private set;}

	public bool Active {
		get {
			if (AI != null) {
				return (energy >= 0 && !isDead);
			}
			return false;
		}
	}

	private void Awake() {
		CreatureManager.AddCreature(this);
		MapLocation = new Vector2Int((int)transform.position.x, (int)transform.position.y);

		try {
			AnimHandler = GetComponent<CreatureAnimationHandler>();
		} catch {
			Debug.Log(this.gameObject.name + " has no AnimationHandler");
		}

		try {
			AI = GetComponent<AI_Base>();
		} catch {
			//This is a player
		}
	}

	private void CheckDeath() {
		if (health <= 0) {
			AnimHandler.SetState("death");
			isDead = true;
		}
	}


	/* PUBLIC METHODS */

	public void UpdateLocation(Vector3 location) {
		//
		MapLocation = new Vector2Int((int)location.x, (int)location.y);
	}

	public void UpdateLocation(Vector2Int location) {
		MapLocation = location;
	}

	public int MeleeAttack() {
		return attackPower;
	}

	public void Strike(int damage) {
		health -= damage;
		CheckDeath();
	}

	public void Recharge() {
		//Used to replenish energy up to the maximum amount. Should be called at end of each round.
		//TODO: Extra check to see if creature is allowed to recharge (ie not sleeped/stunned/dead)

		if (!isDead) {
			energy = Mathf.Min(maxEnergy, energy + energyRecharge);
		}
	}

	public bool Deplete(int amount) {
		//Used to deplete energy. Called by each action.
		//Returns true if there's enough energy; action may still go thru, but could force user to wait for energy to
		//recharge
		if (amount <= energy) {
			energy -= amount;
			return true;
		} else {
			return false;
		}
	}

	public bool CheckEnemies(int faction) {
		//This can be fleshed out later if we need more faction interaction -- 1 way alliances, summons, etc
		return (factionID != faction);
	}


}