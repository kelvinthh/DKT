using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractiveLoadSceneButton : MonoBehaviour, IInteractiveObject {

    [SerializeField] private bool isShortAction = false;

    private float gazeTimer = 2.0f;
    private float gazeTimerFromEditor; //empty float that will store the gazeTimer value, used to reset the gazeTimer value on GazeExit()
    [SerializeField] private int sceneToLoad; //int representing the scene to load by buildIndex

    private bool gazingAt = false; //flag

    public void Action()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(LoadSceneAsync());
    }

    public void GazeEnter()
    {
        gazingAt = true;
    }

    public void GazeExit()
    {
        gazingAt = false;
        gazeTimer = GetTimerDuration();
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
        gazeTimer = GetTimerDuration();
        gazeTimerFromEditor = gazeTimer;
    }
	
	void Update () {
        GazeTimer();	
	}

    private float GetTimerDuration()
    {
        if (isShortAction)
        {
            return 1.5f;
        }

        return 2.0f;
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
