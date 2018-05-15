using UnityEngine;
using System.Collections;

public class CenterOfMass : MonoBehaviour {

    public Vector3 com;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
