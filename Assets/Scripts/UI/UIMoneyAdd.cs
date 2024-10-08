using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIMoneyAdd : MonoBehaviour
{
    int lastFrameMoneyAmount;
    [SerializeField]private TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI slideText;

    [SerializeField] RectTransform startPos;

    public bool doShowOnFirstTime;
    bool isFirstTime = true;

    public Color full;
    public Color empty;
    public float slideDistance;
    public float slideTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastFrameMoneyAmount == GameManager.main.revenue) return;

        if (!doShowOnFirstTime && isFirstTime)
        {
            isFirstTime = false;
            lastFrameMoneyAmount = GameManager.main.revenue;
            text.text = $"${GameManager.main.revenue}";
            return;
        }

        text.text = $"${GameManager.main.revenue}";
        StartCoroutine(MoneyAdd());

        lastFrameMoneyAmount = GameManager.main.revenue;
    }

    IEnumerator MoneyAdd()
    {
        slideText.text = $"+${OrderManager.main.getCurrentRecipe().price}";
        RectTransform rectTransform = slideText.GetComponent<RectTransform>();

        float startTime = Time.time;
        float endTime = startTime + slideTime;

        float startHeight = startPos.anchoredPosition.y;
        float endHeight = startHeight - slideDistance;
        while(Time.time < endTime)
        {
            slideText.color = Color.Lerp(empty, full, (endTime - Time.time) / slideTime);
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y = Mathf.Lerp(endHeight, startHeight, (endTime - Time.time) / slideTime );
            rectTransform.anchoredPosition = pos;
            yield return null;
        }
    }
}
