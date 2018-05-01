using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnCollision : MonoBehaviour {

    [SerializeField] private BoxCollider chairCollider;
    [SerializeField] private BoxCollider blockMacsCollider;

    private bool playerIsPresent = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("playermovedhere (trig)");
        playerIsPresent = true;
        blockMacsCollider.enabled = false;
        chairCollider.enabled = false;
    }


    private void OnTriggerExit(Collider other)
    {

        playerIsPresent = false;
        blockMacsCollider.enabled = true;
        chairCollider.enabled = true;
    }
   

}
