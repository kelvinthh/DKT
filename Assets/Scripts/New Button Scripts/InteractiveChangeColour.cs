using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChangeColour : MonoBehaviour, IInteractiveObject 
{

    [SerializeField] 
    private float gazeTimer = 1.0f; //Seconds taken to activate interactive event

    private bool gazingAt = false; //flag 

    void Start()
    {

    }
    void Update()
    {
        GazeTimer();
    }

    public void Action()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazeTimer = 1.0f;
        gazingAt = false;

    }

    public void GazeTimer()
    {
        if (gazingAt)
        {
            gazeTimer -= Time.deltaTime;

            if (gazeTimer <= 0)
            {
                Action();
            }
        }
    }
}
