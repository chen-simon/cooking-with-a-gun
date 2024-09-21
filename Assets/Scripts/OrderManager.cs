using System;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance {get; private set;}
    [SerializeField]private RecipeListSO recipeListSO;
    public bool activeOrder = false;
    [SerializeField]private List<RecipeSO> orderRecipeList = new List<RecipeSO>();
    public event EventHandler OnRecipeSpwaned; 

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
        ClearOrderList();
        orderRecipeList.Add(recipeListSO.recipeSOList[index]); 
        OnRecipeSpwaned?.Invoke(this, EventArgs.Empty);
    }

    public void ClearOrderList()
    {
        orderRecipeList.Clear();
    }

    public List<RecipeSO> getOrderList(){
        return orderRecipeList;
    }
}
