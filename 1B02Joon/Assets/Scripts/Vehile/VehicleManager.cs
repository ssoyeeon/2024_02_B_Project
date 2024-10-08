using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicles;

    public Car car;                 //�Ĥ��ǳͤ��� �����ä� 
    public Bicycle bicycle;         //�Ĥ��Ǥ��ä�Ʈ �����ä� 

    float Timer;                    //Ÿ�̸� �����ä� 

    void Update()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].Move();
        }
        Timer -= Time.deltaTime;        //Ÿ�̸� ī�줤���� 
        if(Timer <= 0 )
        {
            for(int i = 0;i < vehicles.Length; i++)
            {
                vehicles[i].Horn();
            }
            //car.Move();
            //car.Horn();

            //bicycle.Move();
            //bicycle.Horn();

            Timer = 1.0f;       //1�ʷ� �����Ѥ����֤ä��� 
        }
    }
}
