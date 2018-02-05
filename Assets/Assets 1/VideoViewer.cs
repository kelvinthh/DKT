using UnityEngine;
using System.Collections;

public class VideoViewer: MonoBehaviour
{
    public GameObject Sphere;
    public int numberOfFrames = 0;
    public float frameRate = 30;

    private Texture2D[] frames;

    void Start()
    {
        // load the frames
        frames = new Texture2D[numberOfFrames];
        for (int i = 0; i < numberOfFrames; ++i)
            frames[i] = (Texture2D)Resources.Load(string.Format("frame{0:d4}", i + 1));
    }

    void Update()
    {
        int currentFrame = (int)(Time.time * frameRate);
        if (currentFrame >= frames.Length)
            currentFrame = frames.Length - 1;
        Sphere.GetComponent<Renderer>().material.mainTexture = frames[currentFrame];
    }
}