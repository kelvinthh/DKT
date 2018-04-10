using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sg_ShipData
{
    public int health, maxHealth;
    public string name;
    public sg_ShipClass shipClass;
    public sg_ShipDifficulty difficulty;
    public int shipId;

    public sg_ShipData()
    {
        health = 20;
        maxHealth = 20;
        name = "New Ship";
        shipClass = sg_ShipClass.Fighter;
        difficulty = sg_ShipDifficulty.Normal;
        shipId = 0;
    }
}

public enum sg_ShipDifficulty
{
    Easy,
    Normal,
    Hard,
    Boss,
    Player
}

public enum sg_ShipClass
{
    Fighter,
    Frigate
}