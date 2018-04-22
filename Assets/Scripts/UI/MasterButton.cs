using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEngine.SceneManagement;
public enum ButtonType { LOADSCENE, TELEPORT };

public class MasterButton : MonoBehaviour, IInteractiveObject {

    [SerializeField] public ButtonType buttonAction;
    [SerializeField] private float gazeTimer = 2.0f;
    [SerializeField] public bool hasProgressBar;

    [SerializeField] public Image progressBar;
    [SerializeField] public GameObject teleportTo;

    private float barProgress = 0.0f;
    private float barSpeed = 50.0f;

    [SerializeField] Transform player;

    private bool gazingAt = false;

    public void Action()
    {
        switch(buttonAction)
        {
            case ButtonType.LOADSCENE:
                SceneManager.LoadScene(1);
                break;

            case ButtonType.TELEPORT:
                
                break;
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

    public void GazeTimer()
    {
        if (gazingAt)
        {
            gazeTimer -= Time.deltaTime;

            UpdateBarProgress();

            if (gazeTimer <= 0)
            {
                Action();
            }
        }
    }

    public void UpdateBarProgress()
    {
        if (barProgress < 100)
        {
             barProgress += barSpeed * Time.deltaTime;
             print(barProgress);
        }

        if (progressBar != null) { progressBar.fillAmount = barProgress / 100; }
       
    }

    private float GetProgressBarDuration()
    {
        return 100 / gazeTimer;
    }

    // Use this for initialization
    void Start () {
        GetProgressBarDuration();
        progressBar = GetComponent<Image>();
        player = GameObject.Find("PlayerWithRadialProg").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        GazeTimer();
        
	}
}