using Menu;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmortalState : MonoBehaviour
{
    [SerializeField] private BoxCollider collider;
    [SerializeField] private CheckColision checkColision;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;

    private bool isInmortal;

    private void Start()
    {
        uiManager.nextLevel += ChangeInmortalState;
    }
    void Update()
    {
        
        if(isInmortal) 
        {
            currentTime += Time.deltaTime;

            if(currentTime > maxTime) 
            {

                ChangeInmortalState();
            }
        }
    }

    private void ChangeInmortalState()
    {
        isInmortal = !isInmortal;

        collider.enabled = !isInmortal;
        checkColision.enabled = !isInmortal;

        currentTime = 0;
    }
}