using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    public RenderTexture renderTexture;
    public string url = null;
	// Use this for initialization
	void Start () {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
        videoPlayer.targetTexture = renderTexture;
        //Outsource audio output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign AudioSource to play audio
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        audioSource.volume = 1.0f;

        //Pre-buffer the video
        videoPlayer.Prepare();

        videoPlayer.Play();
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
