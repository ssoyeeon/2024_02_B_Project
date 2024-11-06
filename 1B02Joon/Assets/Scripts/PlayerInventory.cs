using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //각ㄱㅏㄱ의 아이템 개수를 저자ㅇㅎㅏㄴㅡㄴ 변ㅅㅜ 
    public int crystalCount = 0;        //크리스타ㄹ 개수 
    public int plantCount = 0;          //식ㅁㅜㄹ 개수 
    public int bushCount = 0;
    public int treeCount = 0;
    
    //아이템ㅇㅡㄹ 추가하느ㄴ 함ㅅㅜ, 아이템 종ㄹㅠㅇㅔ 따라 해다ㅇ 아이템ㅇㅢ 개수를 증ㄱㅏㅅㅣ킴 
    public void AddItem(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"크리스탈 획득! 현재 개수 : {crystalCount}");
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"식물 획득! 현재 개수 : {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"수풀 획득! 현재 개수 : {bushCount}");
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"나무 획득! 현재 개수 : {treeCount}");
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=====인벤토리=====");
        Debug.Log($"크리스탈:{crystalCount}개");
        Debug.Log($"식물:{plantCount}개");
        Debug.Log($"수풀:{bushCount}개");
        Debug.Log($"나무:{treeCount}개");
        Debug.Log("================");
    }
}
