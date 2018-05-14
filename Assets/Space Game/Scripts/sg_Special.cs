using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Special : MonoBehaviour {

    public int value;
    public sg_SpecialType type;
    private sg_SpecialType m_prevType;
    public Sprite healthSprite, damageSprite, shieldSprite;

    private SpriteRenderer m_renderer;

    public float rotateSpeed = 50f;
    public float spawnTime = 5f;
    public float despawnTime = 10f;
    private float m_despawnTimer = 0f;

    private Transform m_child;
    private SphereCollider m_col;

    private sg_GameManager gm;

    public bool spawned = false;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        m_col = GetComponent<SphereCollider>();
        gm = GameObject.Find("GM").GetComponent<sg_GameManager>();
        m_child = transform.GetChild(0).GetComponent<Transform>();
        m_renderer = m_child.GetComponent<SpriteRenderer>();
        SetImage();
        Spawn();
    }

    private void SetImage()
    {
        switch (type)
        {
            case sg_SpecialType.Health:
                m_renderer.sprite = healthSprite;
                break;
            case sg_SpecialType.Damage:
                m_renderer.sprite = damageSprite;
                break;
            case sg_SpecialType.Shield:
                m_renderer.sprite = shieldSprite;
                break;
            default:
                break;
        }

        m_prevType = type;
    }

    private void Update()
    {
        m_child.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (m_prevType != type) SetImage();

        m_despawnTimer += Time.deltaTime;
        if (spawned)
        {
            if (m_despawnTimer >= despawnTime)
            {
                Despawn();
            }
        }
        else
        {
            if (m_despawnTimer >= spawnTime)
            {
                Spawn();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sg_ShipAi ship = other.gameObject.GetComponent<sg_ShipAi>();
        if (ship != null && other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case sg_SpecialType.Health:
                    ship.data.health = Mathf.Clamp(ship.data.health + value, 0, ship.data.maxHealth);
                    Debug.Log("Applied " + value + " health to '" + ship.data.name + "'");
                    break;
                case sg_SpecialType.Damage:
                    break;
                case sg_SpecialType.Shield:
                    break;
                default:
                    break;
            }

            Despawn();
        }
    }

    public void Spawn()
    {
        if(!m_col || !m_renderer)
        {
            Setup();
        }

        transform.position = RadiusTools.SpecialRandomPosition();

        m_col.enabled = true;
        m_renderer.enabled = true;
        m_despawnTimer = 0f;
        spawned = true;

        Debug.Log("Spawned " + type + " special " + transform.position);
    }
    public void Despawn()
    {
        if (!m_col || !m_renderer)
        {
            Setup();
        }

        m_col.enabled = false;
        m_renderer.enabled = false;
        m_despawnTimer = 0f;
        spawned = false;
    }
}

public enum sg_SpecialType
{
    Health, Damage, Shield
}