using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_ShipAi : MonoBehaviour {

    public sg_ShipData data;

    [SerializeField]
    private List<GameObject> m_allTargets;
    [SerializeField]
    private List<GameObject> m_targetsInRange;
    public GameObject currentTarget;
    public float maxTargetRange = 5.0f;

    public float tickRate = 0.5f;
    private float tickTimer = 0.0f;

    private sg_ShipMovement m_movement;

    private bool isPlayer = false;

    private Renderer[] renderers;

    private void OnEnable()
    {
        m_movement = GetComponent<sg_ShipMovement>();

        if (data.type == sg_ShipType.Player)
        {
            PlayerPrep();
        }
        else
        {
            EnemyPrep();
        }
    }

    private void PlayerPrep()
    {
        isPlayer = true;
        m_movement.targetObject = GameObject.Find("Player Target");
        gameObject.transform.tag = "Player";
        gameObject.transform.name = "Player";
        renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            if (r.gameObject.name.Contains("Cube"))
            {
                r.material.color = Color.blue;
            }
        }
    }

    private void EnemyPrep()
    {
        gameObject.transform.tag = "Enemy";
        gameObject.transform.name = "Enemy";
    }

    private void Update()
    {
        if (tickTimer <= tickRate)
        {
            TickUpdate();
        }
    }

    public void TickUpdate()
    {
        GetAllTargets();
        GetTargetsInRange();

        if (isPlayer)
        {
            if(m_targetsInRange.Count >= 1)
            {
                GetPriorityTarget();
            }
        }
        else
        {
            currentTarget = m_allTargets[0];
            m_movement.targetObject = currentTarget;
        }

        tickTimer = 0.0f;
    }

    private void GetAllTargets()
    {
        if(isPlayer)
        {
            m_allTargets.Clear();

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                m_allTargets.Add(obj);
            }
        }
        else
        {
            m_allTargets.Clear();

            m_allTargets.Add(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    private void GetTargetsInRange()
    {
        m_targetsInRange.Clear();
        foreach (GameObject target in m_allTargets)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= maxTargetRange)
            {
                m_targetsInRange.Add(target);
            }
        }
    }

    private void GetPriorityTarget()
    {
        currentTarget = m_targetsInRange[0];
    }
}