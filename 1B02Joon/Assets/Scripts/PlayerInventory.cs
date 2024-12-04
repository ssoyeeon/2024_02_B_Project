using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;
    //���������� ������ ������ ���ڤ��������Ѥ� ������ 
    public int crystalCount = 0;        //ũ����Ÿ�� ���� 
    public int plantCount = 0;          //�Ĥ��̤� ���� 
    public int bushCount = 0;
    public int treeCount = 0;

    //�߰��� ������
    public int vegetableStewCount = 0;  //��ä ��Ʃ ���� 
    public int fruitSaledCount = 0;     //���� ������ ����
    public int repairKitCount = 0;      //����ŰƮ ����

    public void AddItem(ItemType itemType, int amount)
    {
        //amount��ŭ ������ AddItem ȣ�� �������̵� �Լ��� �Լ� �̸��� ������ �μ��� �ٸ��� ����

        for(int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }
    
    //�����ۤ��Ѥ� �߰��ϴ��� �Ԥ���, ������ �����Ф��� ���� �ش٤� �����ۤ��� ������ ����������Ŵ 
    public void AddItem(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"ũ����Ż ȹ��! ���� ���� : {crystalCount}");
                break;
            case ItemType.Plant:
                plantCount++;
                Debug.Log($"�Ĺ� ȹ��! ���� ���� : {plantCount}");
                break;
            case ItemType.Bush:
                bushCount++;
                Debug.Log($"��Ǯ ȹ��! ���� ���� : {bushCount}");
                break;
            case ItemType.Tree:
                treeCount++;
                Debug.Log($"���� ȹ��! ���� ���� : {treeCount}");
                break;
            case ItemType.VegetableStew:
                vegetableStewCount++;
                Debug.Log($"��ä ��Ʃ ȹ��! ���� ���� : {vegetableStewCount}");
                break;
            case ItemType.FruitSalad:
                fruitSaledCount++;
                Debug.Log($"���� ������ ȹ��! ���� ���� : {fruitSaledCount}");
                break;
            case ItemType.RepairKit:
                repairKitCount++;
                Debug.Log($"���� ŰƮ ȹ��! ���� ���� : {repairKitCount}");
                break;

        }
    }

    //�������� �����ϴ� �Լ� 
    public bool Removeitem(ItemType itemType, int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    crystalCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ���! ���� ���� : {crystalCount}");
                    return true;
                }
                break;
            case ItemType.Plant:
                if (plantCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    plantCount -= amount;
                    Debug.Log($"�Ĺ� {amount} ���! ���� ���� : {plantCount}");
                    return true;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    bushCount -= amount;
                    Debug.Log($"��Ǯ {amount} ���! ���� ���� : {bushCount}");
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    treeCount -= amount;
                    Debug.Log($"���� {amount} ���! ���� ���� : {treeCount}");
                    return true;
                }
                break;
            case ItemType.FruitSalad:
                if (fruitSaledCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    fruitSaledCount -= amount;
                    Debug.Log($"���� ������ {amount} ���! ���� ���� : {fruitSaledCount}");
                    return true;
                }
                break;
            case ItemType.VegetableStew:
                if (vegetableStewCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    vegetableStewCount -= amount;
                    Debug.Log($"��ä ��Ʃ {amount} ���! ���� ���� : {vegetableStewCount}");
                    return true;
                }
                break;
            case ItemType.RepairKit:
                if (repairKitCount >= amount)  //������ �ִ� ������ ������� Ȯ��
                {
                    repairKitCount -= amount;
                    Debug.Log($"���� ŰƮ {amount} ���! ���� ���� : {repairKitCount}");
                    return true;
                }
                break;
        }

        Debug.Log($"{itemType} �������� �����ؿ�Ф�");
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
        Debug.Log("=====�κ��丮=====");
        Debug.Log($"ũ����Ż:{crystalCount}��");
        Debug.Log($"�Ĺ�:{plantCount}��");
        Debug.Log($"��Ǯ:{bushCount}��");
        Debug.Log($"����:{treeCount}��");
        Debug.Log("================");
    }
}
