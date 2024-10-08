using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : Vehicle
{
    public override void Move()
    {
        base.Move();        //기보ㄴ 함ㅅㅜ 동ㅈㅏㄱ을 base 키우ㅓㄷㅡㄹㅗ 동ㅈㅏㄱ 시키ㄴㄷㅏ.
        // 자저ㄴㄱㅓ 만ㅇㅢ 추가 동ㅈㅏㄱ 
        transform.Rotate(0, Mathf.Sin(Time.time) * 10 * Time.deltaTime, 0);     //시가ㄴ 지나면 회전
    }
    public override void Horn()
    {
        Debug.Log("자저ㄴㄱㅓ 경ㅈㅓㄱ ");
    }
}
