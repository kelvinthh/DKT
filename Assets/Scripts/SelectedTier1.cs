using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SelectedTier1 : MonoBehaviour
{
    public GameObject[] tier1;
    public GameObject[] tier2img;
    public GameObject[] tier2vid;
    public GameObject[] tier2env;
    public GameObject[] activeTier;
    public GameObject objSelected;
    public float Countdown;
    public GameObject Back;
    public bool act1;
    public bool act2;
    public bool act3;
    public bool act4;
    public bool act42;
    public bool act43;
    public bool act5;
    public bool act6;
    public bool act7;
    public bool act8;
    public bool act9;
    public bool act10;
    public bool act11;
    public bool act12;
    public bool act13;
    public bool act14;
    public bool act15;
    public bool act16;
    //Add new element booleans

    private void Start()
    {
        ReticleTimer.selectMade = false;
        
    }

    public void triggered()
    {
        ReticleTimer.selected = true;
    }

    public void nottriggered()
    {
        ReticleTimer.selectMade = false;
        ReticleTimer.selected = false;
        ReticleTimer.timelength = 0.63f;
        act1 = false;
        act2 = false;
        act3 = false;
        act4 = false;
        act42 = false;
        act43 = false;
        act5 = false;
        act6 = false;
        act7 = false;
        act8 = false;
        act9 = false;
        act10 = false;
        act11 = false;
        act12 = false;
        act13 = false;
    }
    private void Update()
    {
        if (ReticleTimer.selected == true && ReticleTimer.selectMade ==false)
        {
            
                ReticleTimer.timelength = ReticleTimer.timelength - Time.deltaTime;
            
            if (ReticleTimer.timelength <= 0)
            {
                ReticleTimer.selectMade = true;
                ReticleTimer.selected = false;
                ReticleTimer.timelength = 0.63f;
                if (act1 == true)
                {
                    Debug.Log("firing");
                    foreach (GameObject wi in tier1)
                    {
                        wi.SetActive(false);
                    }
                    foreach (GameObject wi in tier2env)
                    {
                        wi.SetActive(true);
                    }
                   
                    ReticleTimer.selectMade = true;
                    act1 = false;
                }
                if (act2 == true)
                {
                    foreach (GameObject wi in tier2vid)
                    {
                        wi.SetActive(true);
                        Debug.Log("inst");

                    }
                    foreach (GameObject wi in tier1)
                    {
                        wi.SetActive(false);
                    }

                    
                    ReticleTimer.selectMade = true;
                    act2 = false;

                }

                if (act3 == true)
                {
                    foreach (GameObject wi in tier1)
                    {
                        wi.SetActive(false);
                    }
                    foreach (GameObject wi in tier2img)
                    {
                        wi.SetActive(true);
                    }
                    
                    ReticleTimer.selectMade = true;
                    act3 = false;
                }

                if (act4 == true)
                {
                    Loadingscreen.scene = 1;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act4 = false;
                }
                if (act42 == true)
                {
                    Loadingscreen.scene = 10;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act42 = false;
                }
                if (act43 == true)
                {
                    Loadingscreen.scene = 11;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act43 = false;
                }

                if (act5 == true)
                {
                    Loadingscreen.scene = 2;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act5 = false;
                }

                if (act6 == true)
                {

                    Loadingscreen.scene = 3;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act6 = false;
                }

                if (act7 == true)
                {
                    Loadingscreen.scene = 4;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act7 = false;
                }

                if (act8 == true)
                {
                    Loadingscreen.scene = 5;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act8 = false;
                }

                if (act9 == true)
                {
                    Loadingscreen.scene = 6;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act9 = false;
                }

                if (act10 == true)
                {
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act10 = false;
                }

                if (act11 == true)
                {
                    SceneManager.UnloadSceneAsync(0);
                    Application.Quit();
                    act13 = false;
                }

                if (act12 == true)
                {
                    foreach (GameObject wi in activeTier)
                    {
                        wi.SetActive(false);
                    }
                    foreach (GameObject wi in tier1)
                    {
                        wi.SetActive(true);
                    }
                    Back.SetActive(false);
                    ReticleTimer.selectMade = false;
                    act12 = false;
                }
                if (act13 == true)
                {
                    Loadingscreen.scene = 8;
                    SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync(0);
                    ReticleTimer.selectMade = false;
                    act13 = false;
                }
                if (act14 == true)
                {

                }
            }
        }
        else
        {
            ReticleTimer.selected = false;
        }
        
        
        Debug.Log(ReticleTimer.timelength);
        Debug.Log(ReticleTimer.selectMade);
        Debug.Log(ReticleTimer.selected);
    }

    public void Loaded(Collider other)
    {
        if (other.CompareTag("game"))
        {
            triggered();
            act13 = true;
            ReticleTimer.selected = true;
        }

        if (other.CompareTag("videoTier"))
        {
            if (other.gameObject.name == "video1")
            {
                triggered();
                act4 = true;
                ReticleTimer.selected = true;
            }
            if (other.gameObject.name == "video2")
            {
                triggered();
                act42 = true;
                ReticleTimer.selected = true;
            }
            if (other.gameObject.name == "video3")
            {
                triggered();
                act43 = true;
                ReticleTimer.selected = true;
            }

        }
        if (other.CompareTag("imageTier"))
            {
                if (other.gameObject.name == "image1")
                {
                triggered();
                act5 = true;
                ReticleTimer.selected = true;

            }
                if (other.gameObject.name == "image2")
                {
                triggered();
                act6 = true;
                ReticleTimer.selected = true;
            }
                if (other.gameObject.name == "image3")
                {
                triggered();
                act7 = true;
                ReticleTimer.selected = true;
            }
            }
        if (other.CompareTag("envTier"))
        {
            if (other.gameObject.name == "env1")
            {
                triggered();
                act8 = true;
                ReticleTimer.selected = true;

            }
            if (other.gameObject.name == "env2")
            {
                triggered();
                act9 = true;
                ReticleTimer.selected = true;

            }
            
        }
       
    }
    
    
        
    public void Selected(Collider other)
    {
  
            if (other.CompareTag("Video"))
            {
            triggered();
            act2 = true;
            ReticleTimer.selected = true;
        }

            if (other.CompareTag("Image"))
            {
            triggered();
            act3 = true;
            ReticleTimer.selected = true;
        }

           if (other.CompareTag("Environment"))
           {
            triggered();
            act1 = true;
            ReticleTimer.selected = true;
           }

            if (other.CompareTag("Back"))
            {
            triggered();
            act12 = true;
            ReticleTimer.selected = true;
            
        }


            if (other.CompareTag("Exit"))
            {
            triggered();
            act11 = true;
            ReticleTimer.selected = true;
        }
        }
    }

    

