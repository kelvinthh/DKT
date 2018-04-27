using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnCollision : MonoBehaviour {

    private BoxCollider bc;
    private bool playerIsPresent = false;
    private LocationManager lm;
	// Use this for initialization
	void Start () {
        lm = GameObject.Find("LocationManager").GetComponent<LocationManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("playermovedhere");
        playerIsPresent = true;
        bc.enabled = false;
        lm.UpdateColliders();
    }
    
    public void EnableBoxCollider()
    {
        playerIsPresent = false;
        bc.enabled = true;
    }

    public bool isPlayerPresent
    {
        get
        {
            return isPlayerPresent;
        }
        set
        {
            isPlayerPresent = value;
        }
    }
}
