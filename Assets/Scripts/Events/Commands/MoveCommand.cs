using UnityEngine;
using System.Collections;

public class MoveCommand : Command {
    /**
	 * MOVE COMMAND
	 * Purpose: Action to move to another square.
     * 
     * 
	 */

    private Vector2Int Direction {get;}
    private IEnumerator movement;

    public Vector2Int InverseDirection {
        get {
            return new Vector2Int(Direction.x * -1, Direction.y * -1);
        }
    }

    public MoveCommand(CreatureCombatData source, Vector2Int direction, float length = -1f, string animType = "walk") {
        Source = source;
        Direction = direction;
        AnimationType = animType;
        Groupable = true;

        if (length < 0) {
            Length = GameSettings.animSpeeds["walk"];
        } else {
            Length = length;
        }
    }


    /* PUBLIC METHODS */

    public override bool Execute() {
        Vector2Int target = Source.MapLocation + Direction;
        
        if (CommandHelper.CheckPassableAt(target)) {
            movement = SlideTo(target);
            CommandHandler.Instance.StartCoroutine(movement); //this is sloppy
            Source.AnimHandler.SetAnimation(AnimationType, Length);
            Source.UpdateLocation(target);
            return true;
        } else if (CommandHelper.CheckCreatureAt(target)) {
            CreatureCombatData struck = CreatureManager.GetAtLocation(target);


            if (struck != null && struck.CheckEnemies(Source.factionID)) {
                Command.New("melee", Source, struck, Direction).Execute(); //Attempt to attack
                Debug.Log(Source.displayName + " attacks " + struck.displayName);
                //Settings to have same timing/animation as a regular attack
                Length = GameSettings.animSpeeds["melee"];
                Groupable = false;
                return false;
            }
        }

        Source.transform.eulerAngles = LookDirection.Look(Direction);
        return false;

    }

    public override void Cleanup() {
        if (movement != null) {
            CommandHandler.Instance.StopCoroutine(movement);
            movement = null;
        }
    }

    public override string ToString() {
        return Source.gameObject.name + " : " + Direction;
    }


    /* PRIVATE METHODS */

    private IEnumerator SlideTo(Vector2 location) {
        float step = 0;
        
        Vector3 startPosition = Source.transform.position;
        Source.transform.eulerAngles = LookDirection.Look(Direction);

        while (step < 1) {
            step +=  Time.deltaTime / Length;
            Source.transform.position = Vector2.Lerp(startPosition, location, step);
            yield return null;
        }

        Cleanup();
    }



}
