using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractiveLoadSceneButton : MonoBehaviour, IInteractiveObject {

    [SerializeField] private float gazeTimer = 1.0f;
    [SerializeField] private int sceneToLoad;

    private bool gazingAt = false;

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
    
    void Start () {
		
	}
	
	void Update () {
        GazeTimer();	
	}
}
