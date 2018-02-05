using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImageScript : MonoBehaviour
{

    public static Sprite playimg;
    public static Sprite pauseimg;
    public Sprite ps;
    public Sprite ply;


    void Start()
    {
        playimg = ply;
        pauseimg = ps;
    }
}
