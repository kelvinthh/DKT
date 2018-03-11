using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGrow : MonoBehaviour, IInteractiveObject {

    [SerializeField] private float growRate = 1.0f;
     private float gazeTimer = 1.0f; //Seconds taken to activate interactive event
    private bool gazingAt = false; //flag 
    public void Action()
    {
        
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
            transform.localScale += new Vector3(0, growRate, 0);

            gazeTimer -= Time.deltaTime;

            Action();
        } 
    }

    // Use this for initialization
    void Start () {
        gazeTimer = GetComponent<GazeData>().getGazeTimer;
    }
	
	// Update is called once per frame
	void Update () {
        GazeTimer();
    }

 
}
