using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public static PlayerInputManager Instance {get; private set;}

    public CreatureCombatData playerCharacter;

    public static bool acceptingInput = true;

    public bool hasCommand = false;

    private void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}
	}

    public void Update() {

        if (acceptingInput) {
            if (Input.GetKeyDown("a")) {
                Move(new Vector2Int(-1, 0));
            } else if (Input.GetKeyDown("d")) {
                Move(new Vector2Int(1, 0));
            }

            if (Input.GetKeyDown("w")) {
                Move(new Vector2Int(0, 1));

            } else if (Input.GetKeyDown("s")) {
                Move(new Vector2Int(0, -1));
            }

            if (hasCommand) {
                GameStateManager.CallActions();
                hasCommand = false;
            }
        }
    }

    private void Move(Vector2Int direction) {
        Command moveCommand = Command.New("move", playerCharacter, direction);

        if (moveCommand != null) {
            hasCommand = true;
            CommandHandler.Instance.EnqueueCommand(moveCommand);
        } else {
            hasCommand = false;
        }
    }

}
