using UnityEngine;

public abstract class Command {

	public abstract float Length {get;}
	public virtual CreatureCombatData Source {get;}
	public virtual string AnimationType {get;}

	public abstract bool Execute();
	public abstract void Cleanup();
}
