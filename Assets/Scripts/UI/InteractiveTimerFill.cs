using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveTimerFill : MonoBehaviour, IInteractiveObject {

    private Image progressBar;
    private float currentAmount = 0.0f;
    private float speed = 50.0f;
    private float gazeTimer = 2.0f;
    private bool gazingAt = false;

    [SerializeField] private bool isShortAction = false;
    
    public void Action()
    {
        currentAmount = 0.0f;
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazingAt = false;
        currentAmount = 0.0f;
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
    public void UpdateBarProgress()
    {
        if (gazingAt)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
                print(currentAmount);
            }

            if (progressBar != null) { progressBar.fillAmount = currentAmount / 100; }
        }
    }
    private float GetTimerDuration()
    {
        if (isShortAction)
        {
            speed = 33.3f;
            return 1.5f;
        }

        speed = 50.0f;
        return 2.0f;
    }

    // Use this for initialization
    void Start () {
        gazeTimer = GetTimerDuration();
        progressBar = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        GazeTimer();
        UpdateBarProgress();
        
    }
}
