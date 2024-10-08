using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Recipe recipe;
    public Transform standPostiton;
    public Transform exitPosition;

    [SerializeField] float walkSpeed;
    [SerializeField] float leaveDelay;
    [SerializeField] GameObject OrderUI;

    // Start is called before the first frame update
    void Start()
    {
        WalkIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WalkIn()
    {
        transform.DOMove(standPostiton.position, walkSpeed);
        StartCoroutine(WalkInCoroutine());
    }

    public void WalkOut()
    {
        StartCoroutine(WalkOutCoroutine());
    }

    IEnumerator WalkInCoroutine()
    {
        yield return new WaitForSeconds(walkSpeed);
        OrderUI.SetActive(true);
    }

    IEnumerator WalkOutCoroutine()
    {
        yield return new WaitForSeconds(leaveDelay);

        transform.DOMove(exitPosition.position, walkSpeed);
        OrderUI.SetActive(false);
    }
}
