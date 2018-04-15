using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    public float deathTimer = 10f;

    private void OnEnable()
    {
        m_transform = GetComponent<Transform>();
        m_body = GetComponent<Rigidbody>();
        GameObject.Destroy(gameObject, deathTimer);
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
        m_body.AddForce(m_transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        sg_ShipAi ai = other.gameObject.GetComponent<sg_ShipAi>();

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
