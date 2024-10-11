using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager main {get; private set;}

    [SerializeField]private GameManager gameManager;
    [SerializeField]private List<Recipe> recipeList;
    [SerializeField]private Recipe currentRecipe;

    public bool activeOrder = false;
    public int orderCounter = 0; 
    
    public event EventHandler OnRecipeSpwaned; 
    public event EventHandler OnRecipeFinished;

    private void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
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
        int index = UnityEngine.Random.Range(0, recipeList.Count);
        currentRecipe = recipeList[index]; 
        OnRecipeSpwaned?.Invoke(this, EventArgs.Empty);
    }

    public void OrderFinished()
    {
        OnRecipeFinished?.Invoke(this, EventArgs.Empty);  
        gameManager.CalculateEarnings(currentRecipe);
        orderCounter++;
    }

    public Recipe getCurrentRecipe()
    {
        return currentRecipe;
    }
}
