using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackScript : MonoBehaviour {

    public bool backslt;
    private void Start()
    {
        ReticleTimer.timelength = 0.63f;
        backslt = false;    }

    public void triggered()
    {
        ReticleTimer.selected = true;
    }

    public void nottriggered()
    {
        ReticleTimer.selected = false;
        ReticleTimer.timelength = 0.63f;
        backslt = false;
    }
    private void Update()
    {
        //Debug.Log(ReticleTimer.timelength);
        //Debug.Log(ReticleTimer.selected);

        if (ReticleTimer.selected == true && backslt == true)
        {

            ReticleTimer.timelength = ReticleTimer.timelength - Time.deltaTime;

            if (ReticleTimer.timelength <= 0)
            {
                ReticleTimer.selectMade = true;
                ReticleTimer.selected = false;
                ReticleTimer.timelength = 0.63f;
                BackLevel();
            }
        }
    }

    public void BackLevel()
    {
        Debug.Log(Loadingscreen.scene);
        if (Loadingscreen.scene != 1 && Loadingscreen.scene != 10 && Loadingscreen.scene != 11)
        {
            Loadingscreen.scene2 = Loadingscreen.scene;
            Loadingscreen.scene = 0;
            SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(Loadingscreen.scene2);
        }
        else if (Loadingscreen.scene == 1)
        {
            GameObject.Find("Sphere1").GetComponent<UnloadVideos>().starting = true;
        }
        else if (Loadingscreen.scene == 10)
        {
            GameObject.Find("Sphere2").GetComponent<UnloadVideos>().starting = true;
        }
        else if (Loadingscreen.scene == 11)
        {
            GameObject.Find("Sphere3").GetComponent<UnloadVideos>().starting = true;
        }
    }
    public void backselt()
    {
        backslt = true;
        triggered();
    }
}
