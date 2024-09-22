using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEgg : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        bool status = FriedEggManager.main.CompleteTask(0);

        if (status)
        {
            rb.useGravity = false;
            rb.drag = 2f;

            FriedEggManager.main.friedEgg = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
