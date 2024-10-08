using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;    //���� �ä��Ĥ��Ӥ��ä��� �󤼤����Ѥ� ��Ÿ������ ������ 
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
        currentState?.Exit();       //�˻��ؼ� ȣ�� ���� ?�� IF ����

        currentState = newState;

        currentState.Enter();

        Debug.Log($"���� ��ȯ �Ǵ� ������Ʈ : {newState.GetType().Name}");
    }
}
