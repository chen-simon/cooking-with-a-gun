using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color full;
    public Color empty;

    public float fadeInDelay;
    public float holdTime;
    public float fadeTime;

    public bool useFadeIn;
    public bool useFadeOut;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOutCoroutine()
    {

        text.color = empty;

        yield return new WaitForSeconds(fadeInDelay);

        if (useFadeIn)
        {
            // Fade In
            float startTime = Time.time;
            float endTime = startTime + fadeTime;
            while(Time.time < endTime)
            {
                text.color = Color.Lerp(full, empty, (endTime - Time.time) / fadeTime);
                yield return null;
            }
        }

        // Hold
        yield return new WaitForSeconds(holdTime);

        if (useFadeOut)
        {
            // Fade Out
            float startTime = Time.time;
            float endTime = startTime + fadeTime;
            while(Time.time < endTime)
            {
                text.color = Color.Lerp(empty, full, (endTime - Time.time) / fadeTime);
                yield return null;
            }
            gameObject.SetActive(false);
        }

        text.color = full;
    }
}
