using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking With a Gun/Recipe List")]
public class RecipeListSO : ScriptableObject
{
    public string recipeListName;
    public List<RecipeSO> recipeSOList; 
}
