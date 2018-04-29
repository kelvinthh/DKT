using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum ButtonType { LOADSCENE_ON_ACTION, TELEPORT_ON_ACTION, DISPLAY_UI_ON_GAZE, DISPLAY_UI_ON_ACTION, QUIT_ON_ACTION };

public class MasterButton : MonoBehaviour, IInteractiveObject {

    [SerializeField] private ButtonType buttonAction;
    [SerializeField] private float gazeTimer = 2.0f;
    [SerializeField] private bool hasProgressBar;
    private float gazeTimerToEdit;
    [SerializeField] private int sceneIndex;
    [SerializeField] private Image progressBar;
    [SerializeField] private Transform teleportTo;
    [SerializeField] private GameObject overlay;
    private float barProgress = 0.0f;
    private float barSpeed = 50.0f;

    [SerializeField] Transform player;

    private bool gazingAt = false;

    public void Action()
    {
        switch(buttonAction)
        {
            case ButtonType.LOADSCENE_ON_ACTION:
                SceneToLoad.SceneIndexToLoad = sceneIndex;
                SceneManager.LoadScene(sceneIndex);
                break;

            case ButtonType.TELEPORT_ON_ACTION:
                player.position = teleportTo.position;
                break;
            case ButtonType.DISPLAY_UI_ON_GAZE:
                break;
            case ButtonType.QUIT_ON_ACTION:
                Application.Quit();
                break;
        }
    }

    public void GazeEnter()
    {
        gazingAt = true;

        if (buttonAction == ButtonType.DISPLAY_UI_ON_GAZE && overlay != null)
        {
            overlay.SetActive(true);
        }
    }

    public void GazeExit()
    {
        gazingAt = false;
        barProgress = 0.0f;
        UpdateBarProgress();
        gazeTimerToEdit = gazeTimer;

        if (buttonAction == ButtonType.DISPLAY_UI_ON_GAZE && overlay != null)
        {
            overlay.SetActive(false);
        }
    }

    public void GazeTimer()
    {
        if (gazingAt)
        {
            gazeTimerToEdit -= Time.deltaTime;

            UpdateBarProgress();

            if (gazeTimerToEdit <= 0)
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
        }

        if (progressBar != null) { progressBar.fillAmount = barProgress / 100; }
       
    }

    private float GetProgressBarDuration()
    {
        return 100 / gazeTimer;
    }

    IEnumerator LoadNewScene()
    {
        SceneManager.LoadScene(sceneIndex);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }

    // Use this for initialization
    void Start () {
        GetProgressBarDuration();
        player = GameObject.Find("PlayerNew").GetComponent<Transform>();
        gazeTimerToEdit = gazeTimer;
        UpdateBarProgress();
        if(buttonAction == ButtonType.DISPLAY_UI_ON_GAZE && overlay != null)
        {
            overlay.SetActive(false);
        }
            
    }
	
	// Update is called once per frame
	void Update () {

        GazeTimer();
        
	}
}