﻿using System.Collections;
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
    public float tickTimer = 0.0f;

    private sg_ShipMovement m_movement;

    private bool isPlayer = false;

    public Renderer[] renderers;

    public sg_GameManager gm;

    public bool invincible = false;
    public bool autoTarget = true;

    public sg_Weapon weapon;

    private void OnEnable()
    {
        m_movement = GetComponent<sg_ShipMovement>();
        weapon = GetComponent<sg_Weapon>();

        if (data.difficulty == sg_ShipDifficulty.Player)
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
        m_movement.targetObject = GameObject.FindGameObjectWithTag("Player Target");
        gameObject.transform.tag = "Player";
        gameObject.transform.name = "Player";
        foreach(Renderer r in renderers)
        {
            r.material.color = Color.blue;
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
        else
        {
            tickTimer += Time.deltaTime;
        }

        if (currentTarget)
        {
            if (weapon != null) weapon.TryShoot();
        }
    }

    public void TickUpdate()
    {
        GetAllTargets();
        GetTargetsInRange();

        if (autoTarget)
        {
            if (isPlayer)
            {
                if (m_targetsInRange.Count >= 1)
                {
                    GetPriorityTarget();
                }
            }
            else
            {
                currentTarget = m_allTargets[0];
                m_movement.targetObject = currentTarget;
            } 
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

            m_allTargets.Add(gm.m_playerShip);
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

    public void Damage(int dmg)
    {
        if (invincible) return;

        data.health -= dmg;

        if(data.health <= 0)
        {
            gm.NotifyOfDeath(this);
        }
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }
}