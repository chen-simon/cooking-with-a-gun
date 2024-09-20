using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IShootable
{
    public GameObject eggShellPrefab;
    public GameObject eggInsidesPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeShot()
    {
        Instantiate(eggShellPrefab, transform.position, Quaternion.identity);
        Instantiate(eggInsidesPrefab, transform.position, Quaternion.identity);
    }
}
