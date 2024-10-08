using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구체적인 클ㄹㅐㅅㅡ : 자도ㅇ차 
public class Car : Vehicle
{
    //자동으로 Move()함수도 실행 - Vehicle 상속 받았기 때문
    public override void Horn()
    {
        Debug.Log("자동차 경적");
    }
}
