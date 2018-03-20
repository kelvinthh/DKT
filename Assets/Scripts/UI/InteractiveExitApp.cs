using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveExitApp : MonoBehaviour, IInteractiveObject {

    [SerializeField] private bool isShortAction = false;

    private float gazeTimer = 3.0f; //Seconds taken to activate interactive event
    private bool gazingAt = false; //flag 

    public void Action()
    {
        Application.Quit();
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazeTimer = GetTimerDuration();
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private float GetTimerDuration()
    {
        if (isShortAction)
        {
            return 1.5f;
        }

        return 3.0f;
    }
}
