using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Level
{
    public sg_LevelDifficulty difficulty;
    public List<sg_Wave> waves;
}

public class sg_Wave
{

}

public enum sg_LevelDifficulty
{
    Easy,
    Medium,
    Hard,
    Boss
}