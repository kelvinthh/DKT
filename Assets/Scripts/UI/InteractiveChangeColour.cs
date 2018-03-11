using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChangeColour : MonoBehaviour, IInteractiveObject 
{
    [SerializeField] private bool isShortAction = false;


    private float gazeTimer = 3.0f; //Seconds taken to activate interactive event
    [SerializeField] private Color startColour = Color.cyan; //default colour    
    private Color lerpedColour = Color.black; //colour to lerp to once being gazed at
    

    private Color newColour;

    private bool gazingAt = false; //flag 
    private Renderer renderer; //get the material renderer so it can later be changed
    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (isShortAction)
        {
            gazeTimer = 1.5f;
        }
    }
    void Update()
    {
        GazeTimer(); //check for the users gaze every frame
    }

    public void Action()
    {
        lerpedColour = Color.Lerp(Color.white, Color.black, gazeTimer);
        renderer.material.color = lerpedColour;
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazeTimer = 1.0f;
        renderer.material.color = startColour;

        gazingAt = false;

    }

    public void GazeTimer()
    {
        if (gazingAt)
        {
            gazeTimer -= Time.deltaTime;

            Action();
        }
    }
}
