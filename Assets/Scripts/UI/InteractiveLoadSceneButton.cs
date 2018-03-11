using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractiveLoadSceneButton : MonoBehaviour, IInteractiveObject {

    private float gazeTimer;
    private float gazeTimerFromEditor; //empty float that will store the gazeTimer value, used to reset the gazeTimer value on GazeExit()
    [SerializeField] private int sceneToLoad; //int representing the scene to load by buildIndex

    private bool gazingAt = false; //flag

    public void Action()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazingAt = false;
        gazeTimer = gazeTimerFromEditor;
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
    
    void Start () {
        gazeTimer = GetComponent<GazeData>().getGazeTimer;
        gazeTimerFromEditor = gazeTimer;
    }
	
	void Update () {
        GazeTimer();	
	}
}
