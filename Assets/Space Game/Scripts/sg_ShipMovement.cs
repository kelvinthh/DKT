using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class sg_ShipMovement : MonoBehaviour {

    public float thrusterForce = 10f;
    public float turningSpeed = 10f;
    public float tollerance = 0f;
    public GameObject targetObject;
    private Transform m_transform;
    private Rigidbody m_rb;
    private Transform m_mainChild;
    [SerializeField]
    private float m_distanceFromTarget;
    public AnimationCurve decelerationRamp;     //  The curve the ship uses to control it's deceleration.
    public float decelerationDistance;          //  How close the ship must be from the target before it decelerates.
    [SerializeField]
    private float applyForce = 0f;
    [SerializeField]
    private Vector3 directionToTarget;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
        m_mainChild = transform.GetChild(0);
    }

    private void Update()
    {
        if (targetObject != null)
        {
            m_distanceFromTarget = Vector3.Distance(m_transform.position, targetObject.transform.position);
            directionToTarget = Vector3.Normalize(targetObject.transform.position - m_transform.position);
            if (m_distanceFromTarget >= tollerance)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        if(m_distanceFromTarget <= decelerationDistance)
        {
            //  Decelerate
            applyForce = thrusterForce * (decelerationRamp.Evaluate(m_distanceFromTarget / decelerationDistance));
        }
        else
        {
            applyForce = thrusterForce;
        }
        m_rb.AddForce(directionToTarget * applyForce * Time.deltaTime, ForceMode.Impulse);
        if (m_distanceFromTarget >= tollerance)
        {
            Vector3 targetDir = targetObject.transform.position - m_transform.position;
            float step = turningSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(m_mainChild.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);
            m_mainChild.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
