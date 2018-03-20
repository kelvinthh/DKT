using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_GameManager : MonoBehaviour {

    [SerializeField]
    private int m_currentWave;
    [SerializeField]
    private List<sg_ShipData> m_allEnemies;

    [SerializeField]
    private GameObject m_playerShip;

    public List<sg_Level> levels;

    public void NotifyOfDeath(sg_ShipData ship)
    {

        if(m_allEnemies.Count <= 0)
        {
            EndWave();
        }
    }

    public void EndWave()
    {

    }

    public void SpawnEnemy(sg_ShipData data)
    {

    }


    private void Start()
    {
        m_playerShip = GameObject.FindGameObjectWithTag("Player");
    }
}
