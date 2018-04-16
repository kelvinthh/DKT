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
    private int m_lastShipId = 0;

    public GameObject m_playerShip;

    public List<sg_Level> levels;

    public GameObject fighterPrefab;
    public GameObject frigatePrefab;

    public bool doSpawn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { NextWave(); }
    }

    public void NotifyOfDeath(sg_ShipAi ship)
    {
        if(ship.data.difficulty == sg_ShipDifficulty.Player)
        {
            GameObject.Destroy(ship.gameObject);
            Debug.Log("PLAYER DIED");
            m_playerShip = SpawnPlayer();
        }
        else
        {
            GameObject.Destroy(ship.gameObject);

            if (m_remainingEnemies <= 0)
            {
                NextWave();
            }
        }
    }

    public void SpawnEnemy()
    {
        SpawnEnemy(new sg_ShipData());
    }
    public void SpawnEnemy(sg_ShipData data)
    {
        if (!doSpawn) return;

        GameObject newShip;

        switch (data.shipClass)
        {
            case sg_ShipClass.Fighter:
                newShip = fighterPrefab;
                break;
            case sg_ShipClass.Frigate:
                newShip = frigatePrefab;
                break;
            default:
                newShip = fighterPrefab;
                break;
        }

        sg_ShipAi shipAi = newShip.GetComponent<sg_ShipAi>();
        shipAi.gm = this;
        m_lastShipId++;
        data.shipId = m_lastShipId;
        shipAi.data = data;
        shipAi.invincible = false;
        Vector3 pos = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
        newShip = Instantiate(newShip, pos, Quaternion.identity);
    }

    private GameObject SpawnPlayer()
    {
        GameObject newObject = fighterPrefab;
        sg_ShipAi shipAi = newObject.GetComponent<sg_ShipAi>();
        shipAi.gm = this;
        shipAi.data.name = "Player";
        shipAi.data.maxHealth = 100;
        shipAi.data.health = 100;
        shipAi.data.shipId = 0;
        shipAi.data.difficulty = sg_ShipDifficulty.Player;
        shipAi.invincible = true;
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
