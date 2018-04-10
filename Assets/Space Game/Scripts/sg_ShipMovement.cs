using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class sg_ShipMovement : MonoBehaviour {

    public sg_ShipClass movementType;
    public float thrusterForce = 10f;           //  How much force to apply for directional thrust.
    public float verticalForce = 2.0f;          //  How much force to apply for vertical thrust (frigate).
    public float turningSpeed = 10f;            //  How fast to turn.
    public float tollerance = 5f;               //
    public float verticalTollerance = 0.75f;    //
    public float pitchAngle = 15f;              //  The maximum x-axis pitching angle from 0 (frigate).
    public float decelerationDrag = 4.0f;       //  The drag to apply when decelerating (fighter).
    public GameObject targetObject;             //  The 'target' to follow.
    private Transform m_transform;              //  The ship's main transform.
    private Rigidbody m_rb;                     //  The ship's rigidbody.
    private Transform m_mainChild;              //  The main child of the main transform, e.g. for pitching.
    [SerializeField]
    private float m_distanceFromTarget;         //  The approximate distance between the ship and the target.
    public AnimationCurve decelerationRamp;     //  The curve the ship uses to control it's deceleration.
    public float decelerationDistance;          //  How close the ship must be from the target before it decelerates.
    private Vector3 m_directionToTarget;        //  The normalised vector between the ship and the target.

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
            m_directionToTarget = Vector3.Normalize(targetObject.transform.position - m_transform.position);
            switch (movementType)
            {
                case sg_ShipClass.Fighter:
                    FighterMove();
                    break;
                case sg_ShipClass.Frigate:
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
        float applyForce = 0f;

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
            m_rb.AddForce(m_directionToTarget * applyForce * Time.deltaTime, ForceMode.Impulse);
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
        Vector3 applyForce = Vector3.zero;

        if (m_distanceFromTarget >= tollerance)
        {
            Vector3 forceDirection = new Vector3(m_directionToTarget.x, 0, m_directionToTarget.z);
            applyForce.x = m_directionToTarget.x * thrusterForce;
            applyForce.z = m_directionToTarget.z * thrusterForce;

            Quaternion look = Quaternion.LookRotation(applyForce, Vector3.up);
            Vector3 l = m_transform.eulerAngles;
            l.y = Mathf.LerpAngle(l.y, look.eulerAngles.y, turningSpeed * Time.deltaTime);
            m_transform.eulerAngles = l;
        }

        float targetAngle = 0f;
        Vector3 rot = m_mainChild.localEulerAngles;
        if (targetObject.transform.position.y > transform.position.y + verticalTollerance)
        {
            targetAngle = -pitchAngle;
            applyForce.y = verticalForce;
        }
        else if (targetObject.transform.position.y < transform.position.y - verticalTollerance)
        {
            targetAngle = pitchAngle;
            applyForce.y = -verticalForce;
        }
        else { targetAngle = 0f; }
        targetAngle = Mathf.LerpAngle(rot.x, targetAngle, 1f * Time.deltaTime);
        m_rb.AddForce(applyForce * Time.deltaTime, ForceMode.Impulse);
        m_mainChild.localEulerAngles = new Vector3(targetAngle, rot.y, rot.z);
    }
}