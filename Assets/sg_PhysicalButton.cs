using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
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
    
    public Color activeColor = Color.magenta, inactiveColor = Color.red, nonInteractbleColor = Color.grey;
    private Renderer m_renderer;

    public Vector3 activeScale, inactiveScale;
    public float scaleSpeed = 3f;

    [Range(0,1)]
    public float m_Completion = 0f;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_renderer = GetComponent<Renderer>();
        m_audiosource = GetComponent<AudioSource>();
        inactiveColor = m_renderer.material.color;
        gameObject.layer = LayerMask.NameToLayer("UI");
        PhysicalButtonManager.Add(this);
        m_Completion = 0f;
        m_renderer.material.SetFloat("_Max", GetComponent<BoxCollider>().size.x);
        m_renderer.material.SetFloat("_Input", m_Completion);
    }

    private void Update()
    {
        if (!interactable)
        {
            m_renderer.material.color = nonInteractbleColor;
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
            m_Completion = Mathf.Clamp01(m_triggerTimer / triggerTime);
        }
        else
        {
            targetScale = inactiveScale;
            m_triggerTimer += Time.deltaTime;
            m_Completion -= Time.deltaTime * (1.0f / deselectTime);
            m_Completion = Mathf.Clamp01(m_Completion);

        }

        m_transform.localScale = Vector3.MoveTowards(m_transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
        
        m_renderer.material.SetFloat("_Input", m_Completion);
    }

    private void DetectChange()
    {
        if(isBeingLookedAt && !m_prevBeingLookedAt)
        {
            m_prevBeingLookedAt = true;
            //m_renderer.material.color = activeColor;
            m_triggerTimer = 0f;
            if (m_audiosource && clickSound) m_audiosource.PlayOneShot(lookAtSound);

            PhysicalButtonManager.LookAwayFromOthers(this);

            OnLookedAt.Invoke();
        }
        else if(!isBeingLookedAt && m_prevBeingLookedAt)
        {
            m_prevBeingLookedAt = false;
            //m_renderer.material.color = inactiveColor;
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

public static class PhysicalButtonManager
{
    public static List<sg_PhysicalButton> allButtons;

    public static void Init()
    {
        allButtons = new List<sg_PhysicalButton>();
    }

    public static void Add(sg_PhysicalButton b)
    {
        allButtons.Add(b);
    }

    public static void LookAwayFromOthers(sg_PhysicalButton b)
    {
        foreach(sg_PhysicalButton other in allButtons)
        {
            if(b != other)
            {
                other.LookAwayFrom();
            }
        }
    }
}