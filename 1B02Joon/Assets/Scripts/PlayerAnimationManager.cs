using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    private const string PARAM_IS_MOVING = "isMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string PARAM_IS_FALLING = "IsFalling";
    private const string PARAM_ATTACK_TRIGGER = "Attack";

    void Update()
    {
        UpdateAnimationState();
    }

   

    private void UpdateAnimationState()
    {
        if(stateMachine.currentState != null)
        {
            ResetAllBoolParameters();

            switch(stateMachine.currentState)
            {
                case IdleState:
                    break;
                case MovingState:
                    animator.SetBool(PARAM_IS_MOVING, true);
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING,true);
                    }
                    break;
                case JumpingState:
                    animator.SetBool(PARAM_IS_JUMPING , true);
                    break;
                case FallingState:
                    animator.SetBool(PARAM_IS_FALLING , true);
                    break;
            }
        }
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(PARAM_ATTACK_TRIGGER);
    }

    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVING, false);
        animator.SetBool(PARAM_IS_FALLING, false);
        animator.SetBool(PARAM_IS_JUMPING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
    }
}
