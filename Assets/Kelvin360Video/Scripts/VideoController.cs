using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    public VideoPlayer videoPlayer;
    public int sceneNumber;

	// Use this for initialization

    public void playAndPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }else
        {
            videoPlayer.Play();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
