using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour {

	public static CreatureManager Instance {get; private set;}

	public List<CreatureCombatData> creatures = new List<CreatureCombatData>();
	private static Queue<CreatureCombatData> actingThisTurn;

	private IEnumerator currentTurn;

	private void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}
	}

	/* PUBLIC METHODS */

	public static void AddCreature(CreatureCombatData newCreature) {
		if (Instance.creatures.Contains(newCreature) == false) {
			Instance.creatures.Add(newCreature);
		}
	}

	public static void NewRound() {
		SortCreatureList();
		actingThisTurn = GetActingThisTurn();
		QueueCommands();
	}

	/* PRIVATE METHODS */
	private static Queue<CreatureCombatData> GetActingThisTurn() {
		Queue<CreatureCombatData> actingQueue = new Queue<CreatureCombatData>();
		for (int i = 0; i < Instance.creatures.Count; i++) {
			if (Instance.creatures[i].Active) {
				actingQueue.Enqueue(Instance.creatures[i]);
			}
		}
		return actingQueue;
	}

	private static void QueueCommands() {
		while (actingThisTurn.Count > 0) {
			if (actingThisTurn.Peek().AI != null) {
				Command c = actingThisTurn.Dequeue().AI.NextStep();
				CommandHandler.Instance.EnqueueCommand(c);
			} else {
				actingThisTurn.Dequeue();
			}
		}
	}


	private static void SortCreatureList() {
		Instance.SortByEnergy();
	}

	private void SortByEnergy() {
		for (int i = 1; i < creatures.Count; i++) {
			CreatureCombatData key = creatures[i];
			int j = i - 1;

			while (j >= 0 && creatures[j].energy < key.energy) {
				creatures[j + 1] = creatures[j];
				j = j - 1;
			}
			creatures[j + 1] = key;
		}
	}


}
