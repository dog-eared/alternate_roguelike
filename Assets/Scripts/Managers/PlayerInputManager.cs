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

            /* BASIC MOVEMENT */
            if (movementDirection != Vector2.zero) {
                //if (CommandHelper.CheckCreatureAt(Player.MapLocation + movementDirection)) {
                  //  CreatureCombatData targetData = CommandHelper.GetCreatureAt(Player.MapLocation + movementDirection);
                    //Melee(targetData, movementDirection);
                 //} else {
                    Move(movementDirection);
                 //}
            }
            if (Input.GetKey("space")) {
                Pause();
            }

            /* COMBAT */
            

            /* STATUS EFFECTS */
            if (Player.energy <= 0) {
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
            CommandHandler.Instance.EnqueueCommand(moveCommand);
            hasCommand = true;
        } 
    }

    private void Pause() {
        Command pauseCommand = Command.New("pause", playerCharacter);
        CommandHandler.Instance.EnqueueCommand(pauseCommand);
        hasCommand = true;
    }

    private void Melee(CreatureCombatData target, Vector2Int direction) {
        Command meleeCommand = Command.New("melee", playerCharacter, target, direction);
        CommandHandler.Instance.EnqueueCommand(meleeCommand);
        hasCommand = true;
    }

}
