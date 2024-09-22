using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private OrderManager orderManager;
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
}
