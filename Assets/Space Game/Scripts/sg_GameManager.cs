using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_GameManager : MonoBehaviour {

    [SerializeField]
    private int currentLevel, levelCount;
    [SerializeField]
    private int currentWave, waveCount;
    [SerializeField]
    private int m_remainingEnemies;

    [SerializeField]
    private GameObject m_playerShip;

    public List<sg_Level> levels;

    public GameObject shipPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { NextWave(); }
    }

    public void NotifyOfDeath(sg_ShipData ship)
    {

        if(m_remainingEnemies<= 0)
        {
            NextWave();
        }
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
        GameObject newShip = Instantiate(newObject);
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

        NextWave();
    }

    public void NextLevel()
    {
        levelCount = levels.Count;
        if(currentLevel + 1 >= levelCount)
        {
            currentLevel = 0;
        }
        else
        {
            currentLevel++;
        }

        NextWave();
    }
    public void NextWave()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GameObject.Destroy(enemy);
        }

        waveCount = levels[currentLevel].waves.Count;
        if(currentWave + 1 > waveCount)
        {
            currentWave = 0;
            NextLevel();
        }
        else
        {
            foreach(sg_ShipData ship in levels[currentLevel].waves[currentWave].enemies)
            {
                SpawnEnemy(ship);
            }

            currentWave++;
        }
    }
}
