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

            /*if (Ninja1.Instance.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            Ninja1.Instance.Jump = false;

            if (Ninja2.Instance.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            Ninja2.Instance.Jump = false;

            if (Ninja3.Instance.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            Ninja3.Instance.Jump = false;

            if (Ninja4.Instance.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            Ninja4.Instance.Jump = false;
            */
        }
    
        else if(animator.gameObject.name == "Doggo")
        {
            if (DoggoScript.Instance.OnGround)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            DoggoScript.Instance.Jump = false;
        }
        else if(animator.gameObject.name == "Squirrel")
        {
            if (SquirrelScript.Instance.isGrounded)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            SquirrelScript.Instance.jump = false;
        }
        else if(animator.gameObject.name == "Dragon")
        {
            if (DragonScript.Instance.isGrounded)
            {
                animator.SetBool("land", false);
                animator.ResetTrigger("jump");
            }
            DragonScript.Instance.jump = false;
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
