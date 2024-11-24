using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BudingType
{
    CraftingTable,  //���۴�
    Furnace,        //�뱤��
    Kitchen,        //�ֹ�
    Storage         //â��
}
[System.Serializable]

public class CraftingRecipe
{
    public string itemName;             //���ڤ������� ������ �̸��� 
    public ItemType resultItem;         //������̤� 
    public int resultAmount = 1;        //������̤� ���� 

    public float hungerRestoreAmount;
    public float repairAmount;

    public ItemType[] requiredItems;    //�ʤ��ˤ����� ���� 
    public int[] requiredAmounts;       //�ʤ��ˤ����� ��� ���� 
}

