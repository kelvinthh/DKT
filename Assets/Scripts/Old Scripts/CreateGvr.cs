using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGvr : MonoBehaviour {

    public GameObject Gvr;
    private int counting = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counting = 0;
        foreach (Transform t in transform)
        {            
            if (t.name == "GvrEventSystem(Clone)")
            {
                counting++;
            }
        }
        if (counting == 0)
        {
            Instantiate(Gvr, transform);
        }
    }
}
