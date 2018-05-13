using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Weapon : MonoBehaviour
{
    public int bulletDamage = 5;
    public float fireRate = 5f;
    public Transform bulletSpawnPoint;
    public AudioClip shootSound;
    private AudioSource m_audiosource;

    private sg_ShipAi m_ai;
    private float m_shootTimer = 0f;
    private sg_BulletPool m_bulletPool;

    private void Start()
    {
        m_bulletPool = GameObject.Find("GM").GetComponent<sg_BulletPool>();
        m_audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        m_ai = GetComponent<sg_ShipAi>();
    }

    public void TryShoot()
    {
        if(m_shootTimer <= 0 && bulletSpawnPoint)
        {
            DoShoot();
        }
    }

    private void Update()
    {
        m_shootTimer -= Time.deltaTime;
    }

    private void DoShoot()
    {
        GameObject newBullet = m_bulletPool.Spawn(bulletSpawnPoint.position, bulletSpawnPoint.rotation, false);
        sg_Missile missile = newBullet.GetComponent<sg_Missile>();
        missile.damage = bulletDamage;
        missile.shipId = m_ai.data.shipId;
        newBullet.SetActive(true);
        if (m_audiosource && shootSound) m_audiosource.PlayOneShot(shootSound);
        m_shootTimer = (1.0f / fireRate);
    }
}