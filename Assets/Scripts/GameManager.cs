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

    private void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    void Start()
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
}
