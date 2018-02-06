using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoLoad : MonoBehaviour
{

    public GameObject Sphere;
    public GameObject Viewer;
    public bool videoDone = false;
    public bool videoPause = true;

    // Use this for initialization
    void Start()
    {
        Viewer.SetActive(false);
        var vPlayer = Sphere.AddComponent<UnityEngine.Video.VideoPlayer>();
        if (GameObject.Find("Moon Surface Test").GetComponent<LoadAll>().videoNumber == 1)
        {
            //Sphere.AddComponent<UnityEngine.Video.VideoPlayer>().url = "http://alternatedevelopments.com/testvideo.mp4";
            vPlayer.url = "http://alternatedevelopments.com/testvideo.mp4";
            this.name = "Sphere1";
        }
        else if (GameObject.Find("Moon Surface Test").GetComponent<LoadAll>().videoNumber == 2)
        {
            vPlayer.url = "http://alternatedevelopments.com/Warcraft.mp4";
            this.name = "Sphere2";
        }
        else if (GameObject.Find("Moon Surface Test").GetComponent<LoadAll>().videoNumber == 3)
        {
            vPlayer.url = "http://alternatedevelopments.com/xcom.mp4";
            this.name = "Sphere3";
        }
        vPlayer.prepareCompleted += Prepared;
        vPlayer.Prepare();
        //Sphere.AddComponent<UnityEngine.Video.VideoPlayer>().prepareCompleted += Prepared;
        //Sphere.AddComponent<UnityEngine.Video.VideoPlayer>().Prepare();

        

    }

    void Prepared(UnityEngine.Video.VideoPlayer vPlayer)
    {
        Debug.Log("Hello");
        vPlayer.Pause();
        //vPlayer.Play();
        //StartCoroutine(WaitTime(vPlayer));
    }

    private void Update()
    {
        if (videoPause == false & videoDone == false)
        {
            Viewer.SetActive(true);
            SceneManager.UnloadSceneAsync(7);
            videoDone = true;
        }
        
    }


    IEnumerator WaitTime(UnityEngine.Video.VideoPlayer vPlayer)
    {
        yield return new WaitForSeconds(0.5f);
        vPlayer.Play();
        videoPause = false;
        //Sphere.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
    }

}
