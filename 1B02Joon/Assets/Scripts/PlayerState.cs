using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;        //�󤼤� �ӽä����� ���Ϥ� ������ (���� ������) 
    protected PlayerController playerController;        //�ä��Ĥ��Ӥ��� �����Ѥ��Ǥ����� ���Ϥ� ������ 

    //�����ä��� �󤼤� �ӽä����� �ä��Ĥ��Ӥ��� �����Ѥ��Ǥ��� ������ �ʱ�ȭ 
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;
    }

    //���� �޼���� : ���� Ŭ�������� �ʿ信 ���� ��������Ʈ
    public virtual void Enter() { }     //���� ���� �� ȣ��
    public virtual void Exit() { }      //���� ���� �� ȣ�� 
    public virtual void Update() { }    //�� ������ ȣ��
    public virtual void FixedUpdate() { }   //���� �ð� �������� ȣ�� (���� �����)

    //���� ��ȯ ������ üũ�ϴ� �޼���
    protected void CheckTransitions()
    {
        if(playerController.isGrounded())
        {
            //���� ���� ���� ���� ��ȯ ����
            if(Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                stateMachine.TransitionToState(new MovingState(stateMachine));
            }
            else
            {
                    stateMachine.TransitionToState(new IdleState(stateMachine));
            }
        }
        else
        {
            if(playerController.GetVerticalVelocity() > 0)
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new fallingState(stateMachine));
            }

        }
    }

}

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� �����Ӹ��� ���� ��ȯ ���� üũ 
    }

}
public class MovingState : PlayerState
{
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� �����Ӹ��� ���� ��ȯ ���� üũ 
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}
public class fallingState : PlayerState
{
    public fallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� �����Ӹ��� ���� ��ȯ ���� üũ 
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}
public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //�� �����Ӹ��� ���� ��ȯ ���� üũ 
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}