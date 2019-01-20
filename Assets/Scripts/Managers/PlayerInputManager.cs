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

    private Vector2Int movementDirection;

    private void Awake() {
		//Enforce singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate(this);
		}
	}

    private void Update() {

        movementDirection = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));

        if (acceptingInput) {

            if (movementDirection != Vector2.zero) {
                Move(movementDirection);
            }

            /*
            if (Input.GetKeyDown("a")) {
                Move(new Vector2Int(-1, 0));
            } else if (Input.GetKeyDown("d")) {
                Move(new Vector2Int(1, 0));
            }

            if (Input.GetKeyDown("w")) {
                Move(new Vector2Int(0, 1));
            } else if (Input.GetKeyDown("s")) {
                Move(new Vector2Int(0, -1));
            }*/

            if (Input.GetKey("space")) {
                Pause();
            }

            if (Input.GetKeyDown("j")) {
                Melee(new Vector2Int(1, 0));
                Debug.Log("Did a melee attack");
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
            CommandHandler.Instance.EnqueueCommand(moveCommand);
            hasCommand = true;
        }
    }

    private void Pause() {
        Command pauseCommand = Command.New("pause", playerCharacter);
        CommandHandler.Instance.EnqueueCommand(pauseCommand);
        hasCommand = true;
    }

    private void Melee(Vector2Int direction) {
        Command meleeCommand = Command.New("melee", playerCharacter, direction);
        CommandHandler.Instance.EnqueueCommand(meleeCommand);
        hasCommand = true;
    }

}
