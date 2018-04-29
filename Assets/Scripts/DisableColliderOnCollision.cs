using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnCollision : MonoBehaviour {

    [SerializeField] private BoxCollider bc;
    private bool playerIsPresent = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("playermovedhere");
        playerIsPresent = true;
        bc.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsPresent = false;
        bc.enabled = true;
    }
    public void EnableBoxCollider()
    {
        playerIsPresent = false;
        bc.enabled = true;
    }

}
