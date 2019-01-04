using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CreatureAnimationHandler : MonoBehaviour {
    /**
     * CREATURE ANIMATION HANDLER
     * Purpose: Set animations for parent GameObject. Intent is to have this class handle any character animations
     * shown via the character model/sprite (whereas the GameController's animator will handle spell effects, blood,
     * explosions etc)
     * TODO: If status effects are implemented, they can be handled here.
     */


    private Animator animator;

    /* String label, int will be used by the animator to set the animation */
    private static Dictionary<string, int> animations = new Dictionary<string, int>() {
        {"idle", 0},
        {"walk", 1}
    };

    private void Awake() {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
    }


    /* PUBLIC METHODS */

    public void SetAnimation(string animName, float duration = 0.4f) {
        animator.SetInteger("currentMove", animations[animName]);
        Invoke("ResetAnimation", duration);
    }

    public void SetAnimation(int animIndex, float duration = 0.4f) {
        animator.SetInteger("currentMove", animIndex);
        Invoke("ResetAnimation", duration);
    }

    public void ResetAnimation() {
        animator.SetInteger("currentMove", 0);
    }

}
