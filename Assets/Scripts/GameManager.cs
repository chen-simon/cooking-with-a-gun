using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private OrderManager orderManager;
    [SerializeField]private TextMeshProUGUI money;

    [SerializeField] AudioSource moneySound;

    private int totalEarnings = 0;
    public int revenue = 0;
    void Start()
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

    public void CalculateEarnings(Recipe recipe)
    {
        totalEarnings += recipe.price;
        revenue += recipe.price;
        moneySound.Play();
    }

    public void UpdateMoeny()
    {
        money.text = $"${totalEarnings}";
    }
}
