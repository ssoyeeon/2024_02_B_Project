using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    public BudingType buildingType;                 //건물 타입 설정
    public string buildingName;                     //건물 이름
    public int requiredTree = 5;                    //건물 건설에 필요한 나무 숫자
    public float constructionTime = 2.0f;           //건물 건설 시간

    public bool canBuild = true;                    //건설 가능 변수
    public bool isConstructed = false;              //건설 완료 변수

    private Material bulidingMaterial;              //건물의 메테리얼 참조 

    // Start is called before the first frame update
    void Start()
    {
        bulidingMaterial = GetComponent<MeshRenderer>().material;
        //초기 상태 설정(반투명)
        Color color = bulidingMaterial.color;
        color.a = 0.5f;
        bulidingMaterial.color = color;
    }
    
    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstructed) return;                 //건설 가능, 완료 변수 체크하고 리턴

        if(inventory.treeCount >= requiredTree)                 //건설에 필요한 나무 숫자를 확인
        {
            inventory.Removeitem(ItemType.Tree, requiredTree);  //해당 나무 숫자만큼 차감
            if(FloatingTextManager.Instance != null )
            {
                FloatingTextManager.Instance.Show($"{buildingName} 건설 시작!", transform.position + Vector3.up);

            }
            StartCoroutine(CostructionRoutine());
        }
        else
        {
            if(FloatingTextManager.Instance != null)
            {
                FloatingTextManager.Instance.Show($"나무가 부족합니다! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }

    }
    private IEnumerator CostructionRoutine()
    {
        canBuild = false;
        float timer = 0;
        Color color = bulidingMaterial.color;

        while(timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer/constructionTime);
            bulidingMaterial.color = color;
            yield return null;
        }
        isConstructed = true;
        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.Show($"{buildingName} 건설 완료!", transform.position + Vector3.up);

        }
    }
}
