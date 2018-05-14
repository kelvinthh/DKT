using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private sg_Turret m_turret;
    private float autoShootAngle = 15f;

    public Renderer[] renderers;

    public sg_GameManager gm;

    public bool invincible = false;
    public bool autoTarget = true;

    public sg_Weapon weapon;

    public GameObject[] allChildren;

    private Image m_healthBar;

    private void OnEnable()
    {
        m_movement = GetComponent<sg_ShipMovement>();
        m_turret = GetComponent<sg_Turret>();
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
        m_movement.targetObject = GameObject.Find("Player Target");
        gameObject.transform.name = "Player";
        foreach(GameObject child in allChildren)
        {
            child.transform.tag = "Player";
            child.layer = LayerMask.NameToLayer("Player Ship");
        }
        foreach(Renderer r in renderers)
        {
            r.material.color = Color.blue;
        }
        gm.m_playerShip = gameObject;
        m_healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
    }

    private void EnemyPrep()
    {
        gameObject.transform.name = "Enemy";
        foreach (GameObject child in allChildren)
        {
            child.transform.tag = "Enemy";
            child.layer = LayerMask.NameToLayer("Enemy Ship");
        }
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
            m_turret.targetObject = currentTarget;

            Vector3 targetDir = currentTarget.transform.position - weapon.bulletSpawnPoint.transform.position;
            if (Vector3.Angle(targetDir, weapon.bulletSpawnPoint.transform.forward) <= autoShootAngle)
            {
                if (weapon != null) weapon.TryShoot();
            }
        }
    }

    public void TickUpdate()
    {
        GetAllTargets();
        GetTargetsInRange();

        if (autoTarget)
        {
            if (data.difficulty == sg_ShipDifficulty.Player)
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
        if(data.difficulty == sg_ShipDifficulty.Player)
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
            if (IsPlayer())
            {
                //data.health = data.maxHealth;
                gm.NotifyOfDeath(this);
            }
            else
            {
                gm.NotifyOfDeath(this);
            }
        }

        if (IsPlayer()) { m_healthBar.fillAmount = Mathf.Clamp01((float)data.health / (float)data.maxHealth); }
    }

    public bool IsPlayer()
    {
        return data.difficulty == sg_ShipDifficulty.Player;
    }

    public void Select()
    {
        foreach (Renderer r in renderers)
        {
            r.material.SetInt("_Highlight", 1);
        }
    }
    public void Deselect()
    {
        foreach (Renderer r in renderers)
        {
            r.material.SetInt("_Highlight", 0);
        }
    }
}