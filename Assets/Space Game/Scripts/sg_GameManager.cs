using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_GameManager : MonoBehaviour {

    [SerializeField]
    private int m_currentWave;
    [SerializeField]
    private int m_remainingEnemies;

    [SerializeField]
    private GameObject m_playerShip;

    public List<sg_Level> levels;

    public int GetCurrentWave()
    {
        return m_currentWave;
    }
    public int GetRemainingEnemies()
    {
        return m_remainingEnemies;
    }


    private void Start()
    {
        m_playerShip = GameObject.FindGameObjectWithTag("Player");
    }
}
