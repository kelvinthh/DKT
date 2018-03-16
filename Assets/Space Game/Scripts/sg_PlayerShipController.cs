using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_PlayerShipController : MonoBehaviour {

    public Transform shipTarget;
    public LayerMask mask;
    public float restingDistance = 5f;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, mask))
        {
            shipTarget.transform.position = hit.point;
        }
        else
        {
            shipTarget.transform.position = cam.transform.forward * restingDistance;
        }
    }
}
