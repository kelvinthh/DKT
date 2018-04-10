using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Missile : MonoBehaviour {

    public int damage = 10;
    public float speed = 10f;
    public float turnSpeed = 10f;
    public AnimationCurve turnSpeedCurve;
    public float maxRange = 20f;
    public Transform target;

    private Rigidbody m_body;
    private Transform m_transform;
    public bool shotFromPlayer = false;

    private void OnEnable()
    {
        m_transform = GetComponent<Transform>();
        m_body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (target)
        {
            MissileStuff();
        }
        else
        {
            if (GameObject.Find("Player"))
            {
                target = GameObject.Find("Player").transform;
            }
        }
    }

    void MissileStuff()
    {
        float range = Vector3.Distance(m_transform.position, target.position);
        m_body.velocity = m_transform.forward * speed;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - m_transform.position);
        if(range <= maxRange)
        {
            m_body.MoveRotation(Quaternion.RotateTowards(m_transform.rotation, targetRotation, Mathf.Abs(turnSpeedCurve.Evaluate(range / maxRange) * turnSpeed)));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        sg_ShipAi ai = collision.gameObject.GetComponent<sg_ShipAi>();

        if (ai) Contact(ai);

        Explode();
    }

    void Contact(sg_ShipAi ai)
    {
        if (ai.data.difficulty == sg_ShipDifficulty.Player && shotFromPlayer) return;

        ai.Damage(damage);
    }

    void Explode()
    {

    }
}
