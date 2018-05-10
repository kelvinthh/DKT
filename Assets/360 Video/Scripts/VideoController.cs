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

    [SerializeField] private GameObject statusText;

    private bool buttonPressed = false;
    // Use this for initialization
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void PlayAndPause()
    {
        print("playpause called");
        if (videoPlayer.isPlaying && !buttonPressed)
        {
            videoPlayer.Pause();
            spriteRenderer.sprite = playIcon;
            buttonPressed = true;
        }
        else
        {
            if (!buttonPressed)
            {
                videoPlayer.Play();
                spriteRenderer.sprite = pauseIcon;
                buttonPressed = true;
            }
         
        }
    }

    public void UnpressButton()
    {
        buttonPressed = false;
    }

    public void Back()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
