using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class sg_PhysicalButton : MonoBehaviour {

    private Transform m_transform;

    public bool interactable = true;

    public bool isBeingLookedAt;
    private bool m_prevBeingLookedAt;
    public float triggerTime = 2.0f;
    private float m_triggerTimer;
    public float deselectTime = 0.3f;

    public UnityEvent OnLookedAt;
    public UnityEvent OnLookedAway;
    public UnityEvent OnClicked;

    private AudioSource m_audiosource;
    public AudioClip lookAtSound;
    public AudioClip clickSound;
    
    public Material activeMaterial, inactiveMaterial, nonInteractbleMaterial;
    private Renderer m_renderer;

    public Vector3 activeScale, inactiveScale;
    public float scaleSpeed = 3f;

    public List<sg_PhysicalButton> otherButtons;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_renderer = GetComponent<Renderer>();
        m_audiosource = GetComponent<AudioSource>();
        if (!activeMaterial) activeMaterial = m_renderer.material;
        if (!inactiveMaterial) inactiveMaterial = m_renderer.material;
    }

    private void Update()
    {
        if (!interactable)
        {
            m_renderer.material = nonInteractbleMaterial;
            return;
        }

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
            targetScale = inactiveScale;
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
            if (m_audiosource && clickSound) m_audiosource.PlayOneShot(lookAtSound);

            foreach(sg_PhysicalButton b in otherButtons)
            {
                b.LookAwayFrom();
            }

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
    public void LookAwayFrom()
    {
        isBeingLookedAt = false;
    }
}