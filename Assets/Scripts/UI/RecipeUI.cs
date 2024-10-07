using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private Image iconTemplete;
    [SerializeField] private TextMeshProUGUI RecipeNameText;
    [SerializeField] private Transform StepParent;
    void Awake() {
    iconTemplete.gameObject.SetActive(false);
    }
    public void UpdateUI(Recipe recipe)
    {
        RecipeNameText.text = recipe.recipeName + $" ${recipe.price}";
        foreach(Ingredient ingredient in recipe.ingredientList)
        {
            Image newIcon = GameObject.Instantiate(iconTemplete);
            newIcon.transform.SetParent(StepParent, false);
            newIcon.sprite = ingredient.sprite;
            newIcon.gameObject.SetActive(true);
        }

    }
}
