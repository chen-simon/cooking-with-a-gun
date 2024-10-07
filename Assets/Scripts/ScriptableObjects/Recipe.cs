using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking With a Gun/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public Sprite sprite;
    public int price;
    public List<Ingredient> ingredientList;

}
