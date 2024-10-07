using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager main;

    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform exitPosition;
    [SerializeField] List<Transform> standPositions;
    [SerializeField] float minSpawnDelay;
    [SerializeField] float maxSpawnDelay;

    public Customer currentCustomer;

    private void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    void Start()
    {
        OrderManager.main.OnRecipeFinished += OrderManager_OnRecipeFinished;
    }

    public void SpawnCustomer(Recipe recipe)
    {
        GameObject customerObj = Instantiate(customerPrefab, startPosition.position, Quaternion.Euler(0, -180, 0), transform);
        Customer customer = customerObj.GetComponent<Customer>();
        customer.recipe = recipe;
        customer.exitPosition = exitPosition;
        customer.standPostiton = standPositions[Random.Range(0, standPositions.Count - 1)];

        currentCustomer = customer;
    }

    private void OrderManager_OnRecipeFinished(object sender, System.EventArgs e){
        CustomerLeave();
    }

    void CustomerLeave()
    {
        if (currentCustomer) currentCustomer.WalkOut();
        StartCoroutine(SpawnCustomerCoroutine());
    }

    IEnumerator SpawnCustomerCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        SpawnCustomer(OrderManager.main.getCurrentRecipe());
    }
}
