using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking With a Gun/Ingredient")]
public class Ingredient : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;

}
