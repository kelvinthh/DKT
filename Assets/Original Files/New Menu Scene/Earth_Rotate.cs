using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth_Rotate : MonoBehaviour {

    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, speed * Time.deltaTime);
	}
}
