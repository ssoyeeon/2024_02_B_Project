using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;    //현재 플ㄹㅔㅇㅣㅇㅓㅇㅢ 상ㅌㅐㄹㅡㄹ 나타내느ㄴ 변ㅅㅜ 
    public PlayerController PlayerController;       //PlayerController Chamzo

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();    //game Object PlayerController Chamzo

    }
    void Start()
    {
        TransitionToState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.Update();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }

    public void TransitionToState(PlayerState newState)
    {
        currentState?.Exit();       //검사해서 호출 종료 ?는 IF 조건

        currentState = newState;

        currentState.Enter();

        Debug.Log($"상태 전환 되는 스테이트 : {newState.GetType().Name}");
    }
}
