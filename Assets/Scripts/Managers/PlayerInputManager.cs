using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public CreatureCombatData playerCharacter;
    public static bool acceptingInput = true;

    public void Update() {

        if (acceptingInput) {
            if (Input.GetKeyDown("a")) {
                CommandHandler.Instance.EnqueueCommand(new MoveCommand(playerCharacter, new Vector2Int(-1, 0)));
            } else if (Input.GetKeyDown("d")) {
                CommandHandler.Instance.EnqueueCommand(new MoveCommand(playerCharacter, new Vector2Int(1, 0)));
            }

            if (Input.GetKeyDown("w")) {
                CommandHandler.Instance.EnqueueCommand(new MoveCommand(playerCharacter, new Vector2Int(0, 1)));
            } else if (Input.GetKeyDown("s")) {
                CommandHandler.Instance.EnqueueCommand(new MoveCommand(playerCharacter, new Vector2Int(0, -1)));
            }

            if (Input.GetKeyDown("f")) {
                GameStateManager.CallActions();
            }
        }
    }

}
