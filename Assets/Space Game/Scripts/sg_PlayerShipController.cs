using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_PlayerShipController : MonoBehaviour {

    public Transform shipTarget;
    public LayerMask mask;
    public float restingDistance = 5f;
    public Transform cam;

    private void Start()
    {
        if(!cam) cam = Camera.main.transform;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, mask))
        {
            GameObject hitObject = hit.collider.gameObject;
            shipTarget.transform.position = hit.point;
            Debug.DrawLine(cam.transform.position, hit.point, Color.green);
        }
        else
        {
            shipTarget.transform.position = cam.transform.forward * restingDistance;
            Debug.DrawLine(cam.transform.position, shipTarget.transform.position, Color.yellow);
        }
    }
}
