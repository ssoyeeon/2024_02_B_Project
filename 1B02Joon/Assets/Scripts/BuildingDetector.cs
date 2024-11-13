using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;        //�ǹ� ���� ����
    private Vector3 lastPosition;           //�÷��̾��� ������ ��ġ ����
    private float moveThreshole = 0.1f;     //�̵� ���� �Ӱ谪
    private ConstructibleBuilding currentNearbyBuliding;    //���� ������ �ִ� �Ǽ� ������ �ǹ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPosition, transform.position) > moveThreshole)
        {
            CheckForBuilding();
            lastPosition = transform.position;
        }
        if(currentNearbyBuliding != null && Input.GetKeyDown(KeyCode.F))
        {
            currentNearbyBuliding.StartConstruction(GetComponent<PlayerInventory>());
        }
    }
    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float colsestDistance = float.MaxValue;
        ConstructibleBuilding closestIBuilding = null;

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();
            if (building != null && building.canBuild && !building.isConstructed)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);
                if (distance < colsestDistance)
                {
                    colsestDistance = distance;
                    closestIBuilding = building;
                }
            }
        }

        if (closestIBuilding != currentNearbyBuliding)
        {
            currentNearbyBuliding = closestIBuilding;
            if (currentNearbyBuliding != null)
            {
                if(FloatingTextManager.Instance != null)
                {
                    FloatingTextManager.Instance.Show(
                        $"[F]Ű�� {currentNearbyBuliding.buildingName} �Ǽ�(����{currentNearbyBuliding.requiredTree} �� �ʿ�)" ,
                        currentNearbyBuliding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
