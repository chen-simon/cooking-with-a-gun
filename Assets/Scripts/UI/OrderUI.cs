using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer foodItem;
    [SerializeField] Customer customer;
    
    void Update()
    {
        foodItem.sprite = customer.recipe.sprite;
    }
}
