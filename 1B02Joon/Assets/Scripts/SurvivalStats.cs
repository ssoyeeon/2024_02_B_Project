using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 100;
    public float currentHunger;
    public float hungerDecreaseRate = 1;

    [Header("Space Suit Settings")]
    public float maxSuitDurability = 100;
    public float currentSuitDurability;
    public float havestingDamage = 5.0f;
    public float craftingDamage = 3.0f;

    private bool isGameOver = false;
    private bool isPaused = false;
    private float hungerTimer = 0;

    void Start()
    {
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
    }


    void Update()
    {
        if (isGameOver || isPaused) return;

        hungerTimer += Time.deltaTime;

        if(hungerTimer >= 1.0f)
        {
            currentHunger = Mathf.Max(0, currentHunger - hungerDecreaseRate);
            hungerTimer = 0;

            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (currentHunger <= 0 || currentSuitDurability <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        isGameOver = true;
        Debug.Log("�÷��̾� ���!");
        //TODO : ��� ó�� �߰� ���� ���� UI, ������ �� 
    }

    public void EatFood(float amount)
    {
        if (isGameOver || isPaused) return;

        currentHunger = Mathf.Min(maxHunger, currentHunger + amount);

        if(FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.Show($"��� ȸ�� ���� + {amount}", transform.position + Vector3.up);
        }
    }

    public void RepairSuit(float amount)
    {

        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Min(maxSuitDurability, currentSuitDurability + amount);

        if (FloatingTextManager.Instance != null)
        {
            FloatingTextManager.Instance.Show($"���ֺ� ���� + {amount}", transform.position + Vector3.up);
        }
    }

    public void DamageOnHarvesting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max (0, currentSuitDurability - havestingDamage);
        CheckDeath();
    }

    public void DamageOnCrafting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);
        CheckDeath();
    }

    public float GetHungerPercentage()
    {
        return (currentHunger / maxHunger ) * 100;
    }

    public float GetSuitDurabilityPercentage()
    {
        return (currentSuitDurability / maxSuitDurability) * 100;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void ResetStats()
    {
        isGameOver = false;
        isPaused = false;
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
    }
}
