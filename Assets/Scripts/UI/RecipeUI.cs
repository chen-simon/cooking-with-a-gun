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
    public void UpdateUI( RecipeSO recipeSO)
    {
        RecipeNameText.text = recipeSO.recipeName;
        foreach(IngredientSO ingredientSO in recipeSO.IngredientSOList)
        {
            Image newIcon = GameObject.Instantiate(iconTemplete);
            newIcon.transform.SetParent(StepParent, false);
            newIcon.sprite = ingredientSO.sprite;
            newIcon.gameObject.SetActive(true);
        }

    }
}
