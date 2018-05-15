using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    public GameObject playerPrefab;

    public List<GameObject> enemyShips;

    public bool doSpawn = false;

    private Text m_ScoreText;
    private Text m_RoundText;

    public int gameScore = 0;

    public Canvas hudCanvas;
    public UnityEvent OnPlayerDeath;

    public GameObject[] menuObjects;
    public GameObject playerTarget;

    private void Awake()
    {
        RadiusTools.Init(50);
        PhysicalButtonManager.Init();
    }

    public void StartGame()
    {
        hudCanvas.enabled = true;
        m_RoundText = hudCanvas.transform.Find("Wave Text").GetComponent<Text>();
        m_ScoreText = hudCanvas.transform.Find("Score Text").GetComponent<Text>();
        m_playerShip = SpawnPlayer();
        doSpawn = true;
        NextWave();
    }
    public void EndGame()
    {
        hudCanvas.enabled = false;
        doSpawn = false;

        Debug.Log("Ended game with " + gameScore + " points.");
        int hs = PlayerPrefs.GetInt("Highscore");
        if (gameScore > hs)
        {
            PlayerPrefs.SetInt("Highscore", gameScore);
            Debug.Log("NEW HIGHSCORE!");
        }
        else
        {
            Debug.Log("Highscore is " + hs + ".");
        }

        gameScore = 0;


        float nPFactor = menuObjects[0].transform.position.magnitude;
        Vector3 nP = playerTarget.transform.position.normalized;
        nP.y = 0;
        nP *= nPFactor;
        foreach (GameObject g in menuObjects)
        {
            g.transform.position = nP;
            g.transform.LookAt(nP * 2);
        }
    }

    public void NotifyOfDeath(sg_ShipAi ship)
    {
        if(ship.data.difficulty == sg_ShipDifficulty.Player)
        {
            //GameObject.Destroy(ship.gameObject);
            Debug.Log("PLAYER DIED");
            foreach(GameObject s in enemyShips)
            {
                if (s == null) break;
                GameObject.Destroy(s);
            }
            doSpawn = false;
            GameObject.Destroy(ship.gameObject);
            EndGame();
            OnPlayerDeath.Invoke();
        }
        else
        {
            enemyShips.Remove(ship.gameObject);
            GameObject.Destroy(ship.gameObject);

            m_remainingEnemies--;

            gameScore += ship.data.scoreValue;
            m_ScoreText.text = "Score: " + gameScore.ToString();

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
        Vector3 pos = RadiusTools.FindSpawnPosition(30, 40, 10);
        newShip = Instantiate(newShip, pos, Quaternion.identity);

        m_remainingEnemies++;
        enemyShips.Add(newShip);
    }

    private GameObject SpawnPlayer()
    {
        GameObject newObject = playerPrefab;
        sg_ShipAi shipAi = newObject.GetComponent<sg_ShipAi>();
        shipAi.gm = this;
        shipAi.data.name = "Player";
        shipAi.data.maxHealth = 100;
        shipAi.data.health = 100;
        shipAi.data.shipId = 0;
        shipAi.data.difficulty = sg_ShipDifficulty.Player;
        shipAi.invincible = false;
        return GameObject.Instantiate(newObject);
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

        m_RoundText.text = "Wave: " + currentWave.ToString() + " / " + waveCount.ToString();
    }
}