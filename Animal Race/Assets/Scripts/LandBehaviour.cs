﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (animator.gameObject.name.Contains("Ninja"))
        {
            Ninja n = animator.gameObject.GetComponent<Ninja>();
            if (n.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            n.Jump = false;
        }
    
        else if (animator.gameObject.name.Contains("Doggo"))
        {
            DoggoScript n = animator.gameObject.GetComponent<DoggoScript>();
            if (n.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            n.Jump = false;
        }
        else if (animator.gameObject.name.Contains("Squir"))
        {
            SquirrelScript n = animator.gameObject.GetComponent<SquirrelScript>();
            if (n.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            n.Jump = false;
        }
        else if (animator.gameObject.name.Contains("Dragon"))
        {
            DragonScript n = animator.gameObject.GetComponent<DragonScript>();
            if (n.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            n.Jump = false;
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
