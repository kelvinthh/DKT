using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PausePlay : MonoBehaviour {

    public GameObject Sphere;

    [SerializeField]
    private SpriteRenderer button;
    [SerializeField]
    private Sprite playImage;
    [SerializeField]
    private Sprite pauseImage;
    

    // Use this for initialization
    void Start () {
        button = GameObject.Find("PlayIcon").GetComponent<SpriteRenderer>();
        //playImage = GameObject.Find("Sphere1").GetComponent<ImageScript>().ply;
        //pauseImage = GameObject.Find("Sphere1").GetComponent<ImageScript>().ps;
        if (Loadingscreen.scene == 1)
        {
            Sphere = GameObject.Find("Sphere1");
            playImage = Sphere.GetComponent<ImageScript>().ply;
            pauseImage = Sphere.GetComponent<ImageScript>().ps;
        }
        else if (Loadingscreen.scene == 10)
        {
            Sphere = GameObject.Find("Sphere2");
            playImage = Sphere.GetComponent<ImageScript>().ply;
            pauseImage = Sphere.GetComponent<ImageScript>().ps;
        }
        else if (Loadingscreen.scene == 11)
        {
            Sphere = GameObject.Find("Sphere3");
            playImage = Sphere.GetComponent<ImageScript>().ply;
            pauseImage = Sphere.GetComponent<ImageScript>().ps;
        }

    }
	
	// Update is called once per frame
	public void PlayPause () {

       // playImage = ImageScript.playimg;
      //  pauseImage = ImageScript.pauseimg;

        if (Sphere.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying == true)
        {
            Sphere.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();
            button.sprite = GameObject.Find("Sphere1").GetComponent<ImageScript>().ply;
        }
        else {
            Sphere.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            button.sprite = GameObject.Find("Sphere1").GetComponent<ImageScript>().ps;
        }
    }
}
