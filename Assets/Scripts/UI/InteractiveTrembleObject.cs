using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTrembleObject : MonoBehaviour, IInteractiveObject {

    [SerializeField] private bool isShortAction = false;

    private float gazeTimer = 3.0f;

    private bool gazingAt = false;


    public void Action()
    {
        //play tremble animation...
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazingAt = false;
        gazeTimer = 1.0f;
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

    // Use this for initialization
    void Start () {
        if (isShortAction)
        {
            gazeTimer = 1.5f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        GazeTimer();
	}
}
