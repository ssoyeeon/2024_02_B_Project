using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance { get; private set; }

    [Header("UI References")]
    public Slider hungerSlider;
    public Slider suitDurablilitySlider;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI durabilityText;

    private SurvivalStats survivalStats;

    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindObjectOfType<SurvivalStats>();
        hungerSlider.maxValue = survivalStats.maxHunger;            //슬라이더 최대값 설정
        suitDurablilitySlider.maxValue = survivalStats.maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        hungerSlider.value = survivalStats.currentHunger;
        suitDurablilitySlider.value = survivalStats.currentSuitDurability;
        
        //텍스트 업데이트 퍼센트로 표시
        hungerText.text = $"허기 : {survivalStats.GetHungerPercentage():F0}%";
        durabilityText.text = $"우주복 : {survivalStats.GetSuitDurabilityPercentage():F0}%";

        hungerSlider.fillRect.GetComponent<Image>().color =
            survivalStats.currentHunger < survivalStats.maxHunger * 0.3f ? Color.red : Color.blue;

        suitDurablilitySlider.fillRect.GetComponent<Image>().color =
            survivalStats.currentSuitDurability < survivalStats.maxSuitDurability * 0.3f ? Color.red : Color.blue;
    }
}
