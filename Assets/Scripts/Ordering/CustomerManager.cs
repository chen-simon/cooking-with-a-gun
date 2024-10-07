using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnCustomer(Recipe recipe)
    {
        GameObject customerObj = Instantiate(customerPrefab, transform);
        Customer customer = customerObj.GetComponent<Customer>();
        customer.recipe = recipe;
    }
}
