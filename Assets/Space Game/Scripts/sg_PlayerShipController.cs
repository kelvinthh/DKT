using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_PlayerShipController : MonoBehaviour {

    public Transform shipTarget;
    public LayerMask mask;
    public float restingDistance = 5f;
    public Transform cam;
    public GameObject lookingAt;

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

            if (hitObject.GetComponent<sg_ShipAi>())
            {
                if(hitObject != lookingAt)
                {
                    if(lookingAt) lookingAt.GetComponent<sg_ShipAi>().Deselect();
                    lookingAt = hitObject;
                    lookingAt.GetComponent<sg_ShipAi>().Select();
                }
            }
            else
            {
                if (lookingAt) lookingAt.GetComponent<sg_ShipAi>().Deselect();
                lookingAt = null;
            }
        }
        else
        {
            if (lookingAt) lookingAt.GetComponent<sg_ShipAi>().Deselect();
            lookingAt = null;
            shipTarget.transform.position = cam.transform.forward * restingDistance;
            Debug.DrawLine(cam.transform.position, shipTarget.transform.position, Color.yellow);
        }
    }
}
