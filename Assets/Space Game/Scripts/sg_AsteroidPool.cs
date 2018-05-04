using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_AsteroidPool : MonoBehaviour
{
    public GameObject[] prefabs;

    public int poolSize = 50;

    public List<GameObject> inactive, active, all;

    public float minRadius = 20f;
    public float maxRadius = 30f;
    public float spawnAreaHeight = 30f;

    private Transform m_transform;

    private void Start()
    {
        all = new List<GameObject>();
        active = new List<GameObject>();
        inactive = new List<GameObject>();
        
        m_transform = gameObject.transform;
        for(int i = 0; i < poolSize; i++)
        {
            Vector3 pos = FindSpawnPosition();
            float w = Random.Range(-1000f, 1000f);

            int j = Random.Range(0, 2);
            GameObject newAsteroid = Instantiate(prefabs[j], new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(pos.x * w, pos.y * w, pos.z * w), m_transform);
            newAsteroid.transform.name = "Asteroid_" + i;
            all.Add(newAsteroid);
            active.Add(newAsteroid);
        }
    }

    private Vector3 FindSpawnPosition()
    {
        Vector3 result = Vector3.zero;
        float x = 0, y = 0, z = 0;
        while(Vector3.Magnitude(result) < minRadius || Vector3.Magnitude(result) > maxRadius)
        {
            x = Random.Range(-maxRadius, maxRadius);
            z = Random.Range(-maxRadius, maxRadius);
            result = new Vector3(x, 0, z);
        }
        y = Random.Range(-spawnAreaHeight, spawnAreaHeight);
        result.y = y;
        return result;
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