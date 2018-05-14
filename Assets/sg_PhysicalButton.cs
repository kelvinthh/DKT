using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class sg_PhysicalButton : MonoBehaviour {

    private Transform m_transform;

    public bool isBeingLookedAt;
    private bool m_prevBeingLookedAt;
    public float triggerTime = 2.0f;
    private float m_triggerTimer;
    public float deselectTime = 0.3f;

    public UnityEvent OnLookedAt;
    public UnityEvent OnLookedAway;
    public UnityEvent OnClicked;
    
    public Material activeMaterial, inactiveMaterial;
    private Renderer m_renderer;

    public Vector3 activeScale, inactiveScale;
    public float scaleSpeed = 3f;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_renderer = GetComponent<Renderer>();
        if (!activeMaterial) activeMaterial = m_renderer.material;
        if (!inactiveMaterial) inactiveMaterial = m_renderer.material;
    }

    private void Update()
    {
        DetectChange();

        Vector3 targetScale = m_transform.localScale;

        if (isBeingLookedAt)
        {
            if(m_triggerTimer >= triggerTime)
            {
                OnClicked.Invoke();
            }
            else
            {
                targetScale = activeScale;
            }
            m_triggerTimer += Time.deltaTime;
        }
        else
        {
            if (m_triggerTimer >= deselectTime)
            {
                OnClicked.Invoke();
            }
            else
            {
                targetScale = inactiveScale;
            }
            m_triggerTimer += Time.deltaTime;
        }

        m_transform.localScale = Vector3.MoveTowards(m_transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    private void DetectChange()
    {
        if(isBeingLookedAt && !m_prevBeingLookedAt)
        {
            m_prevBeingLookedAt = true;
            m_renderer.material = activeMaterial;
            m_triggerTimer = 0f;
            OnLookedAt.Invoke();
        }
        else if(!isBeingLookedAt && m_prevBeingLookedAt)
        {
            m_prevBeingLookedAt = false;
            m_renderer.material = inactiveMaterial;
            OnLookedAway.Invoke();
            m_triggerTimer = 0f;
        }
    }

    public void LookAt()
    {
        isBeingLookedAt = true;
    }
}