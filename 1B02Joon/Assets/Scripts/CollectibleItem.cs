using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;       //������ ���� 
    public string itemName;
    public float respawnTime = 30.0f;
    public bool canCollect = true;

    public void CollectiItem(PlayerInventory inventory)
    {
        if (!canCollect) { return; }

        inventory.AddItem(itemType);

        if(FloatingTextManager.Instance != null )
        {
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;
            FloatingTextManager.Instance.Show($"{itemName}", textPosition);
        }
        Debug.Log($"{itemName} ���� �Ϸ�");




    }
}
