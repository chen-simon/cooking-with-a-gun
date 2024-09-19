using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{

    public float flashTime = 0.2f;
    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        sp.enabled = true;
        yield return new WaitForSeconds(flashTime);
        sp.enabled = false;
    }
}
