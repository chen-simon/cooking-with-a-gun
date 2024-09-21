using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform recipeParent;

    [SerializeField] private RecipeUI recipeUItemplete;
    void Start()
    {
        recipeUItemplete.gameObject.SetActive(false);
        OrderManager.Instance.OnRecipeSpwaned += OrderManager_OnRecipeSpawned;

    }
    private void OrderManager_OnRecipeSpawned(object sender, System.EventArgs e){
        UpdateUI();
    }
    private void UpdateUI(){
        foreach(Transform child in recipeParent){
            if(child != recipeUItemplete.transform){
                Destroy(child.gameObject);
            }
        }
        List<RecipeSO>recipeSOList = OrderManager.Instance.getOrderList();
        foreach(RecipeSO recipeSO in recipeSOList){
            RecipeUI recipeUI = GameObject.Instantiate(recipeUItemplete);
            recipeUI.transform.SetParent(recipeParent); 
            recipeUI.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
