using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    public VideoPlayer videoPlayer;
    public int sceneNumber;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite pauseIcon;

    // Use this for initialization
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void playAndPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            spriteRenderer.sprite = playIcon; 
        }else
        {
            videoPlayer.Play();
            spriteRenderer.sprite = pauseIcon;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
