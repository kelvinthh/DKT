using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenLoadLevel : MonoBehaviour {

    int sceneToLoad;

	// Use this for initialization
	void Start () {
        sceneToLoad = SceneToLoad.SceneIndexToLoad;
        StartCoroutine(LoadScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadScene()
    {
        print("loading...");
        yield return new WaitForSeconds(3);
       
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
