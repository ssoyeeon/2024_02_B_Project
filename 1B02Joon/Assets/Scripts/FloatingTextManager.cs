using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;
    public GameObject textPrefab;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject textObj = Instantiate(textPrefab, transform);
        textObj.transform.position = screenPos;

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();
        if(temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textObj));
        }
    }

    private IEnumerator AnimateText(GameObject textobj)
    {
        float duration = 1f;
        float timer = 0;

        Vector3 startPos = textobj.transform.position;
        TextMeshProUGUI temp = textobj.GetComponent<TextMeshProUGUI>();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textobj.transform.position = startPos + Vector3.up * (progress * 50f);

            if(temp != null )
            {
                temp.alpha = progress;
            }

            yield return null;
        }
        Destroy( textobj );
    }
}
