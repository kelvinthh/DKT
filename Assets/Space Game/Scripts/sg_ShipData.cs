using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sg_ShipData
{
    public int health, maxHealth;
    public string name;
    public sg_ShipType type;
    public int shipId;

    public sg_ShipData()
    {
        health = 20;
        maxHealth = 20;
        name = "New Ship";
        type = sg_ShipType.Normal;
        shipId = 0;
    }
}

public enum sg_ShipType
{
    Easy,
    Normal,
    Hard,
    Boss,
    Player
}