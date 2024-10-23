using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartMenu : MonoBehaviour
{   
    public static UIStartMenu main;
    public bool gameStart;
    public float fadeTime;
    [SerializeField] GameObject fadeOut;
    // Start is called before the first frame update
    void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }
    void Start()
    {
        gameStart = false;
        Screen.SetResolution(1366, 768, FullScreenMode.FullScreenWindow);
    }

    // Update is called once per frame
    void Update()
    {
        // Hard code
        if (Input.GetKeyDown(KeyCode.Mouse0)) Play();
    }

    public void Play()
    {
        StartCoroutine(PlayCoroutine());
        gameStart = true;
    }

    IEnumerator PlayCoroutine()
    {
        GetComponent<AudioSource>().Play();
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Template");
    }
}
