using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoStart : MonoBehaviour {

    public GameObject Instance;
    public GameObject Sphere;

    private int startVideo = 0;

	// Use this for initialization
	void Start () {
        Instance = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (startVideo == 4)
        {
            ChangeColour();
        }
	}

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    public void EndCountdown()
    {
        StopCoroutine(Countdown());
        startVideo = 0;
    }

    public void ChangeColour()
    {
        Instance.GetComponent<Renderer>().enabled = false;
        Sphere.GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
    }


    IEnumerator Countdown()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1);
            startVideo++;
        }
    }
}
