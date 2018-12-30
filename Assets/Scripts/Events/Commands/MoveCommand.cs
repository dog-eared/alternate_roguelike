using UnityEngine;
using System.Collections;

public class MoveCommand : Command {

    public override float Length {get;}
    public override CreatureCombatData Source {get;}
    public override string AnimationType {get;}

    private Vector2Int Direction {get;}
    private IEnumerator movement;

    public Vector2Int InverseDirection {
        get {
            return new Vector2Int(Direction.x * -1, Direction.y * -1);
        }
    }

    public MoveCommand(CreatureCombatData source, Vector2Int direction, float length = 0.2f, string animType = "None") {
        Source = source;
        Direction = direction;
        Length = length;
        AnimationType = animType;
    }


    public override bool Execute() {
        Vector3 target = Source.gameObject.transform.position + new Vector3(Direction.x, Direction.y, 0);

        if (CommandHelper.CheckPassableAt(target)) {
            movement = SlideTo(target);
            CommandHandler.Instance.StartCoroutine(movement); //this is sloppy
            Source.UpdateLocation(target);
            return true;
        } else {
            Debug.Log("Collision!");
            return false;
        }

    }

    public override void Cleanup() {
        if (movement != null) {
            CommandHandler.Instance.StopCoroutine(movement);
            movement = null;
        }
    }

    private IEnumerator SlideTo(Vector2 location) {
        float step = 0;
        Vector3 startPosition = Source.transform.position;
        Source.transform.eulerAngles = LookDirection.Look(Direction, Source.transform.eulerAngles);

        while (step < 1) {
            step +=  Time.deltaTime / Length;
            Source.transform.position = Vector2.Lerp(startPosition, location, step);
            yield return null;
        }

        Cleanup();
    }

    public override string ToString() {
        return Source.gameObject.name + " : " + Direction;
    }

}
