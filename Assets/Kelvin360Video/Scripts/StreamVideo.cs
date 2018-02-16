using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    public RenderTexture renderTexture;
	// Use this for initialization
	void Start () {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = "http://192.168.0.34/surfers_360.mp4";
        videoPlayer.targetTexture = renderTexture;
        //Outsource audio output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign AudioSource to play audio
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Pre-buffer the video
        videoPlayer.Prepare();

        videoPlayer.Play();
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
