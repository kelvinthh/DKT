using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeranim : MonoBehaviour {

    public Animator timeran;
	// Use this for initialization
	void Start ()
    {
        timeran = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if (ReticleTimer.selected == true)
        {
            timeran.SetBool("selectedanim", true);
        }
	}

}
