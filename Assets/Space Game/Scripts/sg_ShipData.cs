using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sg_ShipData
{
    public int health, maxHealth;

    public string name = "New Ship";

    public sg_ShipType type;

    public int shipId;
}

public enum sg_ShipType
{
    Easy,
    Normal,
    Hard,
    Boss,
    Player
}