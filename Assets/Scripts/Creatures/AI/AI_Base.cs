using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Base : MonoBehaviour {

	protected CreatureCombatData data;

	private void Awake() {
		SetData();
	}

	private void SetData() {
		if (data == null) {
			data = GetComponent<CreatureCombatData>();
		}
	}

	public abstract Command NextStep(); 

}
