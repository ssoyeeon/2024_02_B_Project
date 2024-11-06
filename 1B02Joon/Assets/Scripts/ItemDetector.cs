using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Crystal,
    Plant,
    Bush,
    Tree,

}
public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;
    private Vector3 lastPosition;
    private float moveThreshole = 0.1f;
    private CollectibleItem currentNearbyItem;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        CheckForItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPosition, transform.position) > moveThreshole)
        {
            CheckForItems();
            lastPosition = transform.position;
        }
        if(currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectiItem(GetComponent<PlayerInventory>());
        }
    }
    
    private void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float colsestDistance = float.MaxValue;
        CollectibleItem closestItem = null;

        foreach(Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();
            if(item != null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if(distance < colsestDistance)
                {
                    colsestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if(closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if(currentNearbyItem != null)
            {
                Debug.Log($"[E] 키를 눌러 {currentNearbyItem.itemName} 수집");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
