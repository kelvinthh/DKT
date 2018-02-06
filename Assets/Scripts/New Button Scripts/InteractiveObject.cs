using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {

    [SerializeField] 
    private float gazeTimer = 1.0f; //Seconds taken to activate interactive event
    private bool gazingAt = false; //flag 


    private void Update()
    {
        if (gazingAt)
        {
            gazeTimer -= Time.deltaTime;

            if(gazeTimer <= 0)
            {
                Action();
            }

        }
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazingAt = false;

    }

    public abstract void Action();
}
