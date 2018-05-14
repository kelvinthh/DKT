using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_BulletPool : MonoBehaviour {

    public GameObject bulletPrefab;
    public int poolSize = 100;

    private List<GameObject> m_inactiveBullets;
    private List<GameObject> m_activeBullets;
    private GameObject m_poolObject;

    private void Awake()
    {
        //m_poolObject = new GameObject("~ Bullet Pool ~");
        //m_inactiveBullets = new List<GameObject>();
        //m_activeBullets = new List<GameObject>();

        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject newBullet = GameObject.Instantiate(bulletPrefab, m_poolObject.transform);
        //    newBullet.SetActive(false);
        //    m_inactiveBullets.Add(newBullet);
        //}
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        //if(m_inactiveBullets.Count <= 0)
        //{
        //    Despawn(m_activeBullets[0]);
        //}

        //GameObject bullet = m_inactiveBullets[0];
        GameObject bullet = GameObject.Instantiate(bulletPrefab);

        bullet.transform.SetParent(null);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        m_inactiveBullets.Remove(bullet);
        bullet.SetActive(true);
        //m_activeBullets.Add(bullet);
        return bullet;
    }
    public GameObject Spawn(Vector3 position, Quaternion rotation, bool doSpawn)
    {
        //if (m_inactiveBullets.Count <= 0)
        //{
        //    Despawn(m_activeBullets[0]);
        //}

        //GameObject bullet = m_inactiveBullets[0];

        GameObject bullet = GameObject.Instantiate(bulletPrefab);

        bullet.transform.SetParent(null);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        //m_inactiveBullets.Remove(bullet);
        bullet.SetActive(doSpawn);
        //m_activeBullets.Add(bullet);
        return bullet;
    }

    public void Despawn(GameObject bullet)
    {
        //bullet.SetActive(false);
        //bullet.transform.SetParent(m_poolObject.transform);
        //bullet.transform.position = Vector3.zero;
        //bullet.transform.rotation = Quaternion.identity;
        //m_inactiveBullets.Add(bullet);
        //m_activeBullets.Remove(bullet);

        GameObject.Destroy(bullet);
    }
}