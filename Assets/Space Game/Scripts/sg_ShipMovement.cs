using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class sg_ShipMovement : MonoBehaviour {

    public sg_ShipMovementType movementType;
    public float thrusterForce = 10f;
    public float verticalForce = 2.0f;
    public float turningSpeed = 10f;
    public float tollerance = 0f;
    public float verticalTollerance = 0.75f;
    public float strafePitchAngle = 15f;
    public float decelerationDrag = 4.0f;
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
    private Vector3 dir;

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
            dir = Vector3.Normalize(targetObject.transform.position - m_transform.position);
            switch (movementType)
            {
                case sg_ShipMovementType.Fighter:
                    FighterMove();
                    break;
                case sg_ShipMovementType.Frigate:
                    FrigateMove();
                    break;
                default:
                    FighterMove();
                    break;
            }
        }
    }

    private void FighterMove()
    {
        if (m_distanceFromTarget >= tollerance)
        {
            float distanceToTarget = Vector3.Distance(m_transform.position, targetObject.transform.position);

            if (m_distanceFromTarget <= decelerationDistance)
            {
                //  Decelerate
                applyForce = 2f;
                m_rb.drag = (decelerationRamp.Evaluate(1 - m_distanceFromTarget / decelerationDistance)) * decelerationDrag;
            }
            else
            {
                applyForce = thrusterForce;
                m_rb.drag = 1f;
            }
            m_rb.AddForce(dir * applyForce * Time.deltaTime, ForceMode.Impulse);
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

    private void FrigateMove()
    {
        float distanceToTarget = Vector3.Distance(m_transform.position, targetObject.transform.position);
        Vector3 applyForce = Vector3.zero;

        if (distanceToTarget >= tollerance)
        {
            Vector3 forceDirection = new Vector3(dir.x, 0, dir.z);
            applyForce.x = dir.x * thrusterForce;
            applyForce.z = dir.z * thrusterForce;

            Quaternion look = Quaternion.LookRotation(applyForce, Vector3.up);
            Vector3 l = m_transform.eulerAngles;
            l.y = Mathf.LerpAngle(l.y, look.eulerAngles.y, turningSpeed * Time.deltaTime);
            m_transform.eulerAngles = l;
        }

        float targetAngle = 0f;
        Vector3 rot = m_mainChild.localEulerAngles;
        if (targetObject.transform.position.y > transform.position.y + verticalTollerance)
        {
            targetAngle = -strafePitchAngle;
            applyForce.y = verticalForce;
        }
        else if (targetObject.transform.position.y < transform.position.y - verticalTollerance)
        {
            targetAngle = strafePitchAngle;
            applyForce.y = -verticalForce;
        }
        else { targetAngle = 0f; }
        targetAngle = Mathf.LerpAngle(rot.x, targetAngle, 1f * Time.deltaTime);
        m_rb.AddForce(applyForce * Time.deltaTime, ForceMode.Impulse);
        m_mainChild.localEulerAngles = new Vector3(targetAngle, rot.y, rot.z);
    }
}

public enum sg_ShipMovementType
{
    Fighter,
    Frigate
}