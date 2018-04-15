using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Weapon : MonoBehaviour
{
    public int bulletDamage = 5;
    public float fireRate = 5f;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    private sg_ShipAi m_ai;
    private float m_shootTimer = 0f;

    private void OnEnable()
    {
        m_ai = GetComponent<sg_ShipAi>();
    }

    public void TryShoot()
    {
        if(m_shootTimer <= 0 && bulletSpawnPoint && bulletPrefab)
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
        Debug.Log("SHOOTY SHOOTY");
        GameObject newBullet = bulletPrefab;
        sg_Missile missile = newBullet.GetComponent<sg_Missile>();
        missile.damage = bulletDamage;
        missile.shotFromPlayer = m_ai.IsPlayer();
        GameObject.Instantiate(newBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        m_shootTimer = (1.0f / fireRate);
    }
}