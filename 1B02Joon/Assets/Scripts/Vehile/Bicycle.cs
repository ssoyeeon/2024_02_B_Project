using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : Vehicle
{
    public override void Move()
    {
        base.Move();        //�⺸�� �Ԥ��� ���������� base Ű��ä��Ѥ��� �������� ��Ű������.
        // ���������� ������ �߰� �������� 
        transform.Rotate(0, Mathf.Sin(Time.time) * 10 * Time.deltaTime, 0);     //�ð��� ������ ȸ��
    }
    public override void Horn()
    {
        Debug.Log("���������� �椸�ä� ");
    }
}
