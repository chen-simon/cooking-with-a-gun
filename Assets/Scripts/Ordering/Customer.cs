using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Recipe recipe;
    public Transform standPostiton;

    [SerializeField] Transform exitPosition;
    [SerializeField] float walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        WalkIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WalkIn()
    {
        transform.DOMove(standPostiton.position, walkSpeed);
    }

    void WalkOut()
    {
        transform.DOMove(exitPosition.position, walkSpeed);
    }
}
