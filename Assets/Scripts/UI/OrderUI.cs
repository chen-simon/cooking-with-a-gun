using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RecipeUI recipeUItemplete;
    [SerializeField] private Transform recipeParent;
    void Start()
    {
        recipeUItemplete.gameObject.SetActive(false);
        OrderManager.main.OnRecipeSpwaned += OrderManager_OnRecipeSpawned;
        OrderManager.main.OnRecipeFinished += OrderManager_OnRecipeFinished;
    }
    private void OrderManager_OnRecipeSpawned(object sender, System.EventArgs e){
        UpdateUI();
    }

    private void OrderManager_OnRecipeFinished(object sender, System.EventArgs e){
        UpdateUI();
    }
    private void UpdateUI(){
        foreach(Transform child in recipeParent)
        {
            if(child != recipeUItemplete.transform)
            {
                Destroy(child.gameObject);
            }
        }
        Recipe currentRecipe = OrderManager.main.getCurrentRecipe();
        RecipeUI recipeUI = GameObject.Instantiate(recipeUItemplete);
        recipeUI.transform.SetParent(recipeParent, false);
        recipeUI.gameObject.SetActive(true);
        recipeUI.UpdateUI(currentRecipe);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
