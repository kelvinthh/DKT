using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour {

    public void OnTriggerEnter(Collider other)    {
        nevigateScript.move = false;
    }

  
}
