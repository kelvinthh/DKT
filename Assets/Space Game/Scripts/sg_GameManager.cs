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

    public GameObject shipPrefab;

    public void NotifyOfDeath(sg_ShipData ship)
    {

        if(m_remainingEnemies<= 0)
        {
            EndWave();
        }
    }

    public void EndWave()
    {

    }

    public void SpawnEnemy()
    {
        SpawnEnemy(new sg_ShipData());
    }
    public void SpawnEnemy(sg_ShipData data)
    {
        GameObject newObject = shipPrefab;
        sg_ShipAi shipAi = newObject.GetComponent<sg_ShipAi>();
        shipAi.data = data;

        GameObject.Instantiate(newObject);
    }

    private GameObject SpawnPlayer()
    {
        GameObject newObject = shipPrefab;
        sg_ShipAi shipAi = newObject.GetComponent<sg_ShipAi>();
        shipAi.data.name = "Player";
        shipAi.data.maxHealth = 100;
        shipAi.data.health = 100;
        shipAi.data.shipId = 0;
        shipAi.data.type = sg_ShipType.Player;

        return GameObject.Instantiate(newObject);
    }


    private void Start()
    {
        m_playerShip = SpawnPlayer();
    }


}
