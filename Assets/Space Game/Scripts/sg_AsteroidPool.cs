using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_AsteroidPool : MonoBehaviour
{
    public GameObject[] prefabs;

    public int poolSize = 50;

    public List<GameObject> inactive, active, all;

    public Vector3 spawnArea;

    private Transform m_transform;

    private void Start()
    {
        all = new List<GameObject>();
        active = new List<GameObject>();
        inactive = new List<GameObject>();
        
        m_transform = gameObject.transform;
        for(int i = 0; i < poolSize; i++)
        {
            float x, y, z, w;
            x = Random.Range(-spawnArea.x, spawnArea.x);
            y = Random.Range(-spawnArea.y, spawnArea.y);
            z = Random.Range(-spawnArea.z, spawnArea.z);
            w = Random.Range(-100f, 100f);

            int j = Random.Range(0, 2);
            GameObject newAsteroid = Instantiate(prefabs[j], new Vector3(x, y, z), Quaternion.Euler(x * w, y * w, z * w), m_transform);
            newAsteroid.transform.name = "Asteroid_" + i;
            all.Add(newAsteroid);
            active.Add(newAsteroid);
        }
    }

    public void Despawn(GameObject ast)
    {
        foreach(GameObject obj in active)
        {
            if(obj.name == ast.name)
            {
                inactive.Remove(ast);
                active.Add(ast);
                ast.SetActive(false);
                return;
            }
        }
    }
}