using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UnloadVideos : MonoBehaviour {

    public GameObject vidPlayer;
    private GameObject createdVidPlayer;

    public bool starting = false;
    public bool create = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (starting == true)
        {
            starting = false;
            if (Loadingscreen.scene != 1 && Loadingscreen.scene != 10 && Loadingscreen.scene != 11)
            {
                Loadingscreen.scene2 = Loadingscreen.scene;
                Loadingscreen.scene = 0;
                SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(Loadingscreen.scene2);
            }
            else if (Loadingscreen.scene == 1)
            {
                Destroy(GameObject.Find("Video Player(Clone)"));//.SetActive(false);
                GameObject.Find("Sphere1").GetComponent<MeshRenderer>().enabled = false;
                Loadingscreen.scene = 0;
                SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
            }
            else if (Loadingscreen.scene == 10)
            {
                Destroy(GameObject.Find("Video Player(Clone)"));
                GameObject.Find("Sphere2").GetComponent<MeshRenderer>().enabled = false;
                Loadingscreen.scene = 0;
                SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
            }
            else if (Loadingscreen.scene == 11)
            {
                Destroy(GameObject.Find("Video Player(Clone)"));
                GameObject.Find("Sphere3").GetComponent<MeshRenderer>().enabled = false;
                Loadingscreen.scene = 0;
                SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
            }
        }
        if (create == true)
        {
            create = false;
            createdVidPlayer = Instantiate(vidPlayer);
            createdVidPlayer.SetActive(true);

            //GameObject.Find("Video Player(Clone)").SetActive(true);

        }
    }
}
