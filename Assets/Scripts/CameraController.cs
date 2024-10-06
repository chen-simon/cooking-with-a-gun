using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Singleton object

    public static CameraController main;
    
    private void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    public void Shake(float duration, float magnitude)
    {
        Debug.Log("Shaking Camera");
        StartCoroutine(ShakeCoroutine(duration, magnitude));    
    }
    
    IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        // move the camera forwards and backwards
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float z = Random.Range(-1f, 1f) * magnitude;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition =
                new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z + z);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = originalPosition;
    }
    
}
