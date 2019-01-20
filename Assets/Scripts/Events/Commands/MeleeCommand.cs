using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCommand : Command {

	private Vector2Int Direction {get;}

	public MeleeCommand(CreatureCombatData source, Vector2Int direction, float length = -1f, string animType = "melee") {
        Source = source;
        Direction = direction;
        AnimationType = animType;

        if (length < 0) {
            Length = GameSettings.animSpeeds["melee"];
        } else {
            Length = length;
        }
    }


	public override bool Execute() {
        Vector3 target = Source.gameObject.transform.position + new Vector3(Direction.x, Direction.y, 0);
		Source.transform.eulerAngles = LookDirection.Look(Direction);

		Source.AnimHandler.SetAnimation(AnimationType, Length);
		Debug.Log("Executed Melee attack");
		return true;

        /*if (CommandHelper.CheckPassableAt(target)) {
            movement = SlideTo(target);
            CommandHandler.Instance.StartCoroutine(movement); //this is sloppy
            
            Source.UpdateLocation(target);
            
        } else {
            Source.transform.eulerAngles = LookDirection.Look(Direction);
            Debug.Log("Collision!");
            return false;
        }*/

    }
	public override void Cleanup() {

	}

}
