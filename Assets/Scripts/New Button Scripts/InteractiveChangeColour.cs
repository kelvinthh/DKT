using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChangeColour : InteractiveObject
{
    public override void Action()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}
