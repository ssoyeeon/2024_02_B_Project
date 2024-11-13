using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;        //건물 감지 범위
    private Vector3 lastPosition;           //플레이어의 마지막 위치 저장
    private float moveThreshole = 0.1f;     //이동 감지 임계값
    private ConstructibleBuilding currentNearbyBuliding;    //현재 가까이 있는 건설 가능한 건물

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
                        $"[F]키로 {currentNearbyBuliding.buildingName} 건설(나무{currentNearbyBuliding.requiredTree} 개 필요)" ,
                        currentNearbyBuliding.transform.position + Vector3.up
                        );
                }
            }
        }
    }
}
