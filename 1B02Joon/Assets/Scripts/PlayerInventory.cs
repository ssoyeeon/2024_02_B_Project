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
