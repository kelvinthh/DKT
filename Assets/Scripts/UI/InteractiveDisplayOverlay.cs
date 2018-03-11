using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDisplayOverlay : MonoBehaviour, IInteractiveObject {

    private float gazeTimer = 1.0f;
    [SerializeField] private GameObject overlay;
    private bool gazingAt = false; //flag 

    public void Action()
    {
        
    }

    public void GazeEnter()
    {
        overlay.SetActive(true);
        //gazingAt = true;
    }

    public void GazeExit()
    {
        overlay.SetActive(false);
        //gazeTimer = 1.0f;
        //gazingAt = false;
    }

    public void GazeTimer()
    {
        if (gazingAt)
        {
            gazeTimer -= Time.deltaTime;

            Action();
        }
    }

    // Use this for initialization
    void Start () {
        overlay.SetActive(false);
        gazeTimer = GetComponent<GazeData>().getGazeTimer;
    }
	
	// Update is called once per frame
	void Update () {
        //GazeTimer();
    }
}
