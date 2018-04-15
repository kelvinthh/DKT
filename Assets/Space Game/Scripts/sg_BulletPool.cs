using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_BulletPool : MonoBehaviour {

    public GameObject bulletPrefab;
    public int poolSize = 100;

    public List<GameObject> m_bullets;
    private GameObject m_poolObject;

    private void Awake()
    {
        m_poolObject = new GameObject("~ Bullet Pool ~");

        for(int i = 0; i < poolSize; i++)
        {
            GameObject newBullet = GameObject.Instantiate(bulletPrefab, m_poolObject.transform);
            newBullet.SetActive(false);
            m_bullets.Add(newBullet);
        }
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = m_bullets[0];
        bullet.transform.SetParent(null);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        m_bullets.Remove(bullet);
        bullet.SetActive(true);
        return bullet;
    }
    public GameObject Spawn(Vector3 position, Quaternion rotation, bool doSpawn)
    {
        GameObject bullet = m_bullets[0];
        bullet.transform.SetParent(null);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        m_bullets.Remove(bullet);
        bullet.SetActive(doSpawn);
        return bullet;
    }

    public void Despawn(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(m_poolObject.transform);
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;
        m_bullets.Add(bullet);
    }
}