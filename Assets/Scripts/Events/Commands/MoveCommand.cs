using UnityEngine;
using System.Collections;

public class MoveCommand : Command {

    public override float Length {get; set;}

    public override GameObject Mover {get;}
    private Vector2Int Direction {get; set;}
    private IEnumerator movement;

    public Vector2Int InverseDirection {
        get {
            return new Vector2Int(Direction.x * -1, Direction.y * -1);
        }
    }

    public MoveCommand(GameObject mover, Vector2Int direction, float length = 0.4f) {
        Mover = mover;
        Direction = direction;
        Length = length;
    }

    public override bool Execute() {
        Vector3 target = Mover.transform.position + new Vector3(Direction.x, Direction.y, 0);

        if (CommandHelper.CheckPassableAt(target)) {
            movement = SlideTo(target);
            GameStateManager.Instance.StartCoroutine(movement); //this is sloppy
            return true;
        } else {
            Debug.Log("Collision!");
            return false;
        }

    }

    public override void Cleanup() {
        if (movement != null) {
            GameStateManager.Instance.StopCoroutine(movement);
            movement = null;
        }
    }

    private IEnumerator SlideTo(Vector2 location) {
        float step = 0;
        Vector3 startPosition = Mover.transform.position;

        while (step < 1) {
            step +=  Time.deltaTime / Length;
            Mover.transform.position = Vector2.Lerp(startPosition, location, step);
            yield return null;
        }

        Cleanup();
    }

    public override string ToString() {
        return Mover.name + " : " + Direction;
    }

}
