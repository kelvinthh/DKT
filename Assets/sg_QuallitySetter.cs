using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_QuallitySetter : MonoBehaviour {

    public int currentQuallity;

    private void Start()
    {
        currentQuallity = QualitySettings.GetQualityLevel();
    }

    public void SetQuallity(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
    }
}
