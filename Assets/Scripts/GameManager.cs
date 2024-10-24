using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    // Start is called before the first frame update
    [SerializeField]private OrderManager orderManager;

    [SerializeField] AudioSource moneySound;

    public int revenue = 0;
    public bool gamePaused = false;
    private void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    void Start()
    {
        if (CalibrationUI.Instance && !CalibrationUI.Instance.IsCalibrated())
        {
            CalibrationUI.Instance.ShowCalibrationScreen();
        }
        else
        {
            StartGame();
        }
    }


    void StartGame()
    {
        orderManager.OrderUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            orderManager.OrderUpdate();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            orderManager.OrderFinished();
        }
    }

    public void CalculateEarnings(Recipe recipe)
    {
        revenue += recipe.price;
        moneySound.Play();
    }

    public void PauseGame()
    {
        if(gamePaused) return;
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void ResumeGame()
    {
        if(!gamePaused) return;
        Time.timeScale = 1f;
        gamePaused = false;
    }
}
