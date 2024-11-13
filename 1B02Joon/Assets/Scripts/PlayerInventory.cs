using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //���������� ������ ������ ���ڤ��������Ѥ� ������ 
    public int crystalCount = 0;        //ũ����Ÿ�� ���� 
    public int plantCount = 0;          //�Ĥ��̤� ���� 
    public int bushCount = 0;
    public int treeCount = 0;

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
            default:
                return 0;
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
