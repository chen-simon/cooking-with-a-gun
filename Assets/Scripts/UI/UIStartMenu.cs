using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1366, 768, FullScreenMode.FullScreenWindow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Screen.SetResolution(1366, 768, FullScreenMode.FullScreenWindow);
        SceneManager.LoadScene("Template");
    }
}
