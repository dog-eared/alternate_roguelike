using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour {

	public static CreatureManager Instance {get; private set;}

	public List<CreatureCombatData> creatures = new List<CreatureCombatData>();

	private void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}
	}

	public static void AddCreature(CreatureCombatData newCreature) {
		if (Instance.creatures.Contains(newCreature) == false) {
			Instance.creatures.Add(newCreature);
		}
	}

	


}
