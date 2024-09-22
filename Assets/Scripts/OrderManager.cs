using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance {get; private set;}
    [SerializeField]private GameManager gameManager;
    [SerializeField]private RecipeListSO recipeListSO;
    [SerializeField]private RecipeSO currentRecipe;
    public bool activeOrder = false;
    public int orderCounter = 0; 
    
    public event EventHandler OnRecipeSpwaned; 
    public event EventHandler OnRecipeFinished;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
        else
        {
            Instance = this;
        }
    }
    private void Update(){
        if(activeOrder == true)
        {
            OrderUpdate();
            activeOrder = false;
        }
    }

    public void OrderUpdate()
    {
        int index = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);
        currentRecipe = recipeListSO.recipeSOList[index]; 
        OnRecipeSpwaned?.Invoke(this, EventArgs.Empty);
    }

    public void OrderFinished()
    {
        OnRecipeFinished?.Invoke(this, EventArgs.Empty);  
        gameManager.CalculateEarnings(currentRecipe);
        gameManager.UpdateMoeny();
        orderCounter++;
    }

    public RecipeSO getCurrentRecipe(){
        return currentRecipe;
    }
}
