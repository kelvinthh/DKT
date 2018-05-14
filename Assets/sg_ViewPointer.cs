using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_ViewPointer : MonoBehaviour {

    public LayerMask mask;

    public GameObject lookingAt;
    private GameObject m_prevLookingAt;
    private Transform m_transform;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_transform.position, m_transform.forward, out hit, Mathf.Infinity, mask))
        {
            GameObject hitObject = hit.collider.gameObject;
            sg_PhysicalButton button = hitObject.GetComponent<sg_PhysicalButton>();
            if (button)
            {
                if (hitObject != lookingAt)
                {
                    lookingAt = hitObject;
                    button.LookAt();
                }
            }
            else
            {
                if (lookingAt) lookingAt.GetComponent<sg_PhysicalButton>().LookAwayFrom();
                lookingAt = null;
            }
        }
        else
        {
            if (lookingAt) lookingAt.GetComponent<sg_PhysicalButton>().LookAwayFrom();
            lookingAt = null;
        }
    }
}
