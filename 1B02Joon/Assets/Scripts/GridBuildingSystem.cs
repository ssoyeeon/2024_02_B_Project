using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GridCell

{
    public bool isOccupied;

    public Vector3Int Position;
    public GameObject Building;
    public GridCell(Vector3Int position)
    {
        Position = position;
        isOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1;

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject buildingPregab;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float maxBulidDistance = 5f;

    [SerializeField] private Grid grid;                  //그리드 선ㅇㅓㄴ 후 받ㅇㅏㅇㅗㄴ다. 

    private GridCell[,] cells;
    private Camera firstPersonCamera;

    void Start()
    {
        CreateGrid();
        firstPersonCamera = playerController.firstPersonCamera;
    }
     void Update()
    {
        Vector3 lookPosition = GetLookPosition();
        if(lookPosition != Vector3.zero)
        {
            Vector3Int gridPosition = grid.WorldToCell(lookPosition);   //보고있는 그리드 셀의 월드 좌표를 받아온다.
            if(isValifGridPosition(gridPosition))                       //그리드 위치가 맞을 경우
            {
                HighlightCell(gridPosition);                            //해당 그리드를 표시해준다.
                if(Input.GetMouseButtonDown(0))
                {
                    PlaceBuliding(gridPosition);

                }
                if (Input.GetMouseButtonDown(1))
                {
                    RemoveBuilding(gridPosition);
                }

            }

        }
    }

    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize);
        cells = new GridCell[width, height];
        Vector3 gridCenter = playerController.transform.position;
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f ,0, height * cellSize/2.0f);

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x,0,z);
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition);
                GameObject cellObject = Instantiate(cellPrefab, worldPosition, cellPrefab.transform.rotation);
                cellObject.transform.SetParent(transform);

                cells[x, z] = new GridCell(cellPosition);
            }
        }
    }

    //그리드 셀에 건물을 배치하는 메서드
    private void PlaceBuliding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];

        if (!cell.isOccupied)
        {
            Vector3 worldPosition = grid.GetCellCenterWorld(gridPosition);
            GameObject buliding = Instantiate(bulidingPrefabs, worldPosition, Quaternion.identity);
            cell.isOccupied = true;
            cell.Building = buliding;
        }
    }

    //플레이어가 그리드 셀에서 건물을 제거하는 함수
    private void RemoveBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];

        if (cell.isOccupied)                            //셀에 건물이 있을 경우 
        {
            Destroy(cell.Building);         //지워버령
            cell.isOccupied = false;     //셀에 건물이 이다고 표시
            cell.Building = null;       //건물 오브젝트 없어요 
        }
    }

    private Vector3 GetLookPosition()
    {
        if (playerController.isFirstPerson)
        {
            Ray ray = new Ray(firstPersonCamera.transform.position, firstPersonCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxBulidDistance))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
                return hitInfo.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.white);
            }
        }
        else
        {
            Vector3 characterPosition = playerController.transform.position;
            Vector3 characterForward = playerController.transform.forward;
            Vector3 rayOrigin = characterPosition + Vector3.up * 1.5f + characterForward * 0.5f;
            Vector3 rayDirection = (characterPosition - Vector3.up).normalized;

            Ray ray = new Ray(rayOrigin, rayDirection);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxBulidDistance))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.blue);
                return hitInfo.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.white);
            }
        }
    }
    private bool isValifGridPosition(Vector3Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width &&
            gridPosition.z >= 0 && gridPosition.z < height;
    }

    //선택 된 셀을 하이라이트 하는 함수
    private void HighlightCell(Vector3Int gridPosition)
    {
        for(int x = 0; x < width; x++)
        {
            for( int z = 0; z < height; z++)
            {
                //Cells 2차원 배열에 선언되어있는 4게임 오브젝트를 ㄱ다져온다.
                GameObject cellObject = cells[x, z].Building != null ? cells[x, z].Building : transform.GetChild(x * height + z).gameObject;
                cellObject.GetComponent<Renderer>().material.color = Color.white;

            }
        }
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        GameObject highlightObject = 
            cell.Building != null?cell.Building : transform.GetChild(gridPosition.x * height + gridPosition.z).gameObject;
        highlightObject.GetComponent<Renderer>().material.color = cell.isOccupied ? Color.red : Color.green;
    }

    private void OnDrawGizmos()     //기즈모르ㄹ 표시해주는 함ㅅㅜ 
    {
        Gizmos.color = Color.blue;
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(x,0,z));
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
            }
        }
    }
}
