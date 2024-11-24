using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;        //상ㅌㅐ 머시ㄴㅇㅔ 대하ㄴ 참ㅈㅗ (이후 구혀ㄴ) 
    protected PlayerController playerController;        //플ㄹㅔㅇㅣㅇㅓ 컨ㅌㅡㄹㅗㄹ러에 대하ㄴ 참ㅈㅗ 
    protected PlayerAnimationManager animationManager;

    //생ㅅㅓㅇ자 상ㅌㅐ 머시ㄴㄱㅘ 플ㄹㅔㅇㅣㅇㅓ 컨ㅌㅡㄹㅗㄹ러 참ㅈㅗ 초기화 
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;
        this.animationManager = stateMachine.GetComponent<PlayerAnimationManager>();
    }

    //가상 메서드들 : 하위 클래스에서 필요에 따라 오버라이트
    public virtual void Enter() { }     //상태 진입 시 호출
    public virtual void Exit() { }      //상태 종료 시 호출 
    public virtual void Update() { }    //매 프레임 호출
    public virtual void FixedUpdate() { }   //고정 시간 간격으로 호출 (물리 연산용)

    //상태 전환 조건을 체크하는 메서드
    protected void CheckTransitions()
    {
        if(playerController.isGrounded())
        {
            //지상에 있을 때의 상태 전환 로직
            if (Input.GetKeyDown(KeyCode.Space))         //스페이스를 눌었을때
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //이동키가 눌렸을때 
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
                stateMachine.TransitionToState(new FallingState(stateMachine));
            }

        }
    }

}

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();     //매 프레임마다 상태 전환 조건 체크 
    }

}
public class MovingState : PlayerState
{
    private bool isRunning;
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        //달리기 입력 확인
        isRunning = Input.GetKey(KeyCode.LeftShift);

        CheckTransitions();     //매 프레임마다 상태 전환 조건 체크 
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
        CheckTransitions();     //매 프레임마다 상태 전환 조건 체크 
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();             //매 프레임마다 상태 전환 조건 체크 
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();              //물리 기반 이동 처리 
    }
}
