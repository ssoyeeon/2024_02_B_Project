using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ü���� Ŭ�������� : �ڵ����� 
public class Car : Vehicle
{
    //�ڵ����� Move()�Լ��� ���� - Vehicle ��� �޾ұ� ����
    public override void Horn()
    {
        Debug.Log("�ڵ��� ����");
    }
}
