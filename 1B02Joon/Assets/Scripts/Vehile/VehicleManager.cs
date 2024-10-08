using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicles;

    public Car car;                 //컴ㅍㅗ넌ㅌㅡ 선ㅇㅓㄴ 
    public Bicycle bicycle;         //컴ㅍㅗㄴㅓㄴ트 선ㅇㅓㄴ 

    float Timer;                    //타이머 선ㅇㅓㄴ 

    void Update()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].Move();
        }
        Timer -= Time.deltaTime;        //타이머 카우ㄴㅌㅡ 
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

            Timer = 1.0f;       //1초로 만ㄷㅡㄹ어주ㅓㅇㅛ 
        }
    }
}
