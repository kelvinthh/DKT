using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChangeLocation : InteractiveObject
{
    [SerializeField]
    private Transform location;
    

    public override void Action()
    {
        GameObject player = GameObject.Find("Player");

        player.GetComponent<Transform>().position = location.position;
    }
}
