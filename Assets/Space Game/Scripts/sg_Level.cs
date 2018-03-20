using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sg_Level
{
    public sg_Difficulty difficulty;
    public List<sg_Wave> waves;
}

[System.Serializable]
public class sg_Wave
{
    public List<sg_Difficulty> enemies;
}

public enum sg_Difficulty
{
    Easy,
    Medium,
    Hard,
    Boss
}