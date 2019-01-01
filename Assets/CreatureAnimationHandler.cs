using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CreatureAnimationHandler : MonoBehaviour {

    private Animator animator;

    private Dictionary<string, int> animations = new Dictionary<string, int>() {
        {"idle", 0},
        {"walk", 1}
    };

    private void Awake() {
        animator = GetComponent<Animator>();
    }

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
