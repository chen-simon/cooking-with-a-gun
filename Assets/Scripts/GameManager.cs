using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private OrderManager orderManager;
    [SerializeField]private TextMeshProUGUI money;
    private int totalEarnings = 0;
    public int revenue = 0;

    void Start()
    {
        if (!CalibrationUI.Instance.IsCalibrated())
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
        UpdateMoeny();
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

    public void CalculateEarnings(RecipeSO recipeSO)
    {
        totalEarnings += recipeSO.price;
        revenue += recipeSO.price;
    }

    public void UpdateMoeny()
    {
        money.text = $"${totalEarnings}";
    }
}
