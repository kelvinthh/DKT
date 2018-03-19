using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_TargetController : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_allTargets;
    [SerializeField]
    private List<GameObject> m_targetsInRange;
    public GameObject currentTarget;
    public float maxTargetRange = 5.0f;

    public float tickRate = 0.5f;
    private float tickTimer = 0.0f;

    private void Update()
    {
        if(tickTimer <= tickRate)
        {
            TickUpdate();
        }
    }

    public void TickUpdate()
    {
        GetAllTargets();
        GetTargetsInRange();

        tickTimer = 0.0f;
    }

    private void GetAllTargets()
    {
        m_allTargets.Clear();

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
        {
            m_allTargets.Add(obj);
        }
    }

    private void GetTargetsInRange()
    {
        m_targetsInRange.Clear();
        foreach(GameObject target in m_allTargets)
        {
            if(Vector3.Distance(target.transform.position, transform.position) <= maxTargetRange)
            {
                m_targetsInRange.Add(target);
            }
        }
    }
}