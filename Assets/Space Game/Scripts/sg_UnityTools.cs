using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class sg_UnityTools
{
    [MenuItem("Space Game/New Level")]
    private static void NewLevel()
    {

    }

    [MenuItem("Space Game/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared Player Prefs");
    }
}