using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;
    //각ㄱㅏㄱ의 아이템 개수를 저자ㅇㅎㅏㄴㅡㄴ 변ㅅㅜ 
    public int crystalCount = 0;        //크리스타ㄹ 개수 
    public int plantCount = 0;          //식ㅁㅜㄹ 개수 
    public int bushCount = 0;
    public int treeCount = 0;

    //추가할 변수들
    public int vegetableStewCount = 0;  //야채 스튜 개수 
    public int fruitSaledCount = 0;     //과일 샐러드 개수
    public int repairKitCount = 0;      //수리키트 개수

    public void AddItem(ItemType itemType, int amount)
    {
        //amount만큼 여러번 AddItem 호출 오버라이드 함수로 함수 이름이 같지만 인수가 다르면 가능

        for(int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }
    
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
            case ItemType.VegetableStew:
                vegetableStewCount++;
                Debug.Log($"야채 스튜 획득! 현재 개수 : {vegetableStewCount}");
                break;
            case ItemType.FruitSalad:
                fruitSaledCount++;
                Debug.Log($"과일 샐러드 획득! 현재 개수 : {fruitSaledCount}");
                break;
            case ItemType.RepairKit:
                repairKitCount++;
                Debug.Log($"수리 키트 획득! 현재 개수 : {repairKitCount}");
                break;

        }
    }

    //아이템을 제거하는 함수 
    public bool Removeitem(ItemType itemType, int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    crystalCount -= amount;
                    Debug.Log($"크리스탈 {amount} 사용! 현재 개수 : {crystalCount}");
                    return true;
                }
                break;
            case ItemType.Plant:
                if (plantCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    plantCount -= amount;
                    Debug.Log($"식물 {amount} 사용! 현재 개수 : {plantCount}");
                    return true;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    bushCount -= amount;
                    Debug.Log($"수풀 {amount} 사용! 현재 개수 : {bushCount}");
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    treeCount -= amount;
                    Debug.Log($"나무 {amount} 사용! 현재 개수 : {treeCount}");
                    return true;
                }
                break;
            case ItemType.FruitSalad:
                if (fruitSaledCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    fruitSaledCount -= amount;
                    Debug.Log($"과일 샐러드 {amount} 사용! 현재 개수 : {fruitSaledCount}");
                    return true;
                }
                break;
            case ItemType.VegetableStew:
                if (vegetableStewCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    vegetableStewCount -= amount;
                    Debug.Log($"야채 스튜 {amount} 사용! 현재 개수 : {vegetableStewCount}");
                    return true;
                }
                break;
            case ItemType.RepairKit:
                if (repairKitCount >= amount)  //가지고 있는 개수가 충분한지 확인
                {
                    repairKitCount -= amount;
                    Debug.Log($"수리 키트 {amount} 사용! 현재 개수 : {repairKitCount}");
                    return true;
                }
                break;
        }

        Debug.Log($"{itemType} 아이템이 부족해용ㅠㅠ");
        return false;
    }

    public int Getitemcount(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                    return bushCount;
            case ItemType.Tree:
                return treeCount;
            case ItemType.VegetableStew:
                return vegetableStewCount;
            case ItemType.FruitSalad:
                return fruitSaledCount;
            case ItemType.RepairKit:
                return repairKitCount;
            default:
                return 0;
        }
    }

    void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }
    
    public void UseItem(ItemType itemType)
    {
        if( Getitemcount(itemType) <= 0)
        {
            return;
        }

        switch (itemType)
        {
            case ItemType.VegetableStew:
                Removeitem(ItemType.VegetableStew, 1);
                survivalStats.EatFood(RecipeList.kitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.FruitSalad:
                Removeitem(ItemType.FruitSalad, 1);
                survivalStats.EatFood(RecipeList.kitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.RepairKit:
                Removeitem(ItemType.RepairKit, 1);
                survivalStats.EatFood(RecipeList.WorkbenchRecipes[0].repairAmount);
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
