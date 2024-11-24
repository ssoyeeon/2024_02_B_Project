using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BudingType
{
    CraftingTable,  //제작대
    Furnace,        //용광로
    Kitchen,        //주방
    Storage         //창고
}
[System.Serializable]

public class CraftingRecipe
{
    public string itemName;             //제자ㄱㅎㅏㄹ 아이템 이르ㅁ 
    public ItemType resultItem;         //결고ㅏㅁㅜㄹ 
    public int resultAmount = 1;        //결고ㅏㅁㅜㄹ 개수 

    public float hungerRestoreAmount;
    public float repairAmount;

    public ItemType[] requiredItems;    //필ㅇㅛㅎㅏㄴ 재료들 
    public int[] requiredAmounts;       //필ㅇㅛㅎㅏㄴ 재료 개수 
}

