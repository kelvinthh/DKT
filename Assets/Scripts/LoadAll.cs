using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAll : MonoBehaviour {

    public int videoNumber = 0;

    // Use this for initialization
    void Start () {        
            StartCoroutine(WaitTime());

    }
	
	// Update is called once per frame
	void Update () {
		
	}



    IEnumerator WaitTime()
    {

        if (!GameObject.Find("Sphere1"))
        {
            videoNumber = 1;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            yield return new WaitForSeconds(0.5f);
        }
        if (!GameObject.Find("Sphere2"))
        {
            videoNumber = 2;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            yield return new WaitForSeconds(0.5f);
        }
        if (!GameObject.Find("Sphere3"))
        {
            videoNumber = 3;
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
        //Sphere.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
    }
}
