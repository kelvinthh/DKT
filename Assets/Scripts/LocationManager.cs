using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour {

    [SerializeField] private GameObject[] waypoints = new GameObject[2];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateColliders()
    {
        print("message recieved");
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i].GetComponent<DisableColliderOnCollision>().isPlayerPresent)
            {
                for(int j = 0; j < waypoints.Length; j++)
                {
                    if (j != i)
                    {
                        waypoints[j].GetComponent<DisableColliderOnCollision>().EnableBoxCollider();
                    }
                }
            }
        }
    }
}
