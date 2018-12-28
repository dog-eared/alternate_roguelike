using UnityEngine;

public abstract class Command {

	public abstract float Length {get; set;}

	public virtual GameObject Mover {get;}

	public abstract bool Execute();
	public abstract void Cleanup();
}
