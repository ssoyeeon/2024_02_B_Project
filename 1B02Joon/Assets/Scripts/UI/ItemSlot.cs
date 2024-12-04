using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI CountText;
    public Button useButton;                    //��� ��Ʈ�� 

    private ItemType itemType;
    private int itemCount;

    public void Setup(ItemType type , int Count)
    {
        itemType = type;
        itemCount = Count;

        itemNameText.text = GetitemDisplayName(type);
        CountText.text = Count.ToString();

        useButton.onClick.AddListener(UseItem);
    }

    private string GetitemDisplayName(ItemType type)
    {
        switch(itemType)
        {
            case ItemType.VegetableStew: return "��ä ��Ʃ";
            case ItemType.FruitSalad: return "���� ������";
            case ItemType.RepairKit: return "���� ŰƮ";
            default: return type.ToString();
        }
    }
    private void UseItem()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        SurvivalStats stats = FindObjectOfType<SurvivalStats>();

        switch(itemType)
        {
            case ItemType.VegetableStew:
                if(inventory.Removeitem(itemType , 1))
                {
                    stats.EatFood(40f);
                    //RefreshInventory
                }
                break;
            case ItemType.FruitSalad:
                if (inventory.Removeitem(itemType, 1))
                {
                    stats.EatFood(50f);
                    //RefreshInventory
                }
                break;
            case ItemType.RepairKit:
                if (inventory.Removeitem(itemType, 1))
                {
                    stats.RepairSuit(25f);
                    //RefreshInventory
                }
                break;
        }
    }
}
