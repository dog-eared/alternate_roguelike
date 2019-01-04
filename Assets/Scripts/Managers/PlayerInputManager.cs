using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    /**
	 * PLAYER INPUT MANAGER
     * Purpose: Handles player character input, creating appropriate commands and blocking
     * input if player not allowed to move
	 */

    public static PlayerInputManager Instance {get; private set;}

    public static CreatureCombatData Player {
        get {
            return Instance.playerCharacter;
        }
    }

    public CreatureCombatData playerCharacter; //Public to allow debug-setting in editor

    public static bool acceptingInput = true;

    public static bool hasCommand = false;

    private void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}
	}

    private void Update() {
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

            if (Input.GetKeyDown("space")) {
                Pause();
            }

            if (hasCommand) {
                GameStateManager.CallActions();
                hasCommand = false;
            }
        }
    }


    /* PRIVATE METHODS */

    private void Move(Vector2Int direction) {
        Command moveCommand = Command.New("move", playerCharacter, direction);

        if (moveCommand != null) {
            hasCommand = true;
            CommandHandler.Instance.EnqueueCommand(moveCommand);
        }
    }

    private void Pause() {
        Command pauseCommand = Command.New("pause", playerCharacter);
        CommandHandler.Instance.EnqueueCommand(pauseCommand);
        hasCommand = true;
    }

}
