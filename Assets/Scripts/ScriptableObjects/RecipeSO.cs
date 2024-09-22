using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite sprite;
    public int price;
    public List<IngredientSO> IngredientSOList;

}
