using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayContextUI : MonoBehaviour
{

    public GameObject cameraObj;
    public GameObject capsuleObj;
    public GameObject[] UI;
    private float displayAngle = 40;

    private bool displayed = false;

    // Update is called once per frame
    void Update()
    {

        if (!displayed) //if UI is not currently displayed...
        {
            if (CheckAngle() > displayAngle && CheckAngle() < 100) //evaluate the cameras current X value within a vertical area...
            {
                DisplayUI();
                displayed = true;
            }
            
        }
        if (displayed) //if UI is currently displayed 
        {
            if (CheckAngle() < displayAngle) //evaluate if the cameras current X value is outside the area
            {
                HideUI();
                displayed = false;
            }
            
        }
        

    }

    private float CheckAngle()
    {
        float xVal;

        xVal = cameraObj.transform.eulerAngles.x;
        
        return xVal;
    }

    void DisplayUI()
    {
        
        transform.position = capsuleObj.transform.position;//match UI pos to player pos

        transform.rotation = Quaternion.Euler(0, cameraObj.transform.rotation.eulerAngles.y, 0);//match UI rotation to player rot

        foreach (GameObject wi in UI) //display UI elements...
        {
            wi.SetActive(true);
        }
    }
    void HideUI()
    {  
            foreach (GameObject wi in UI) //hide UI elements...
            {
                wi.SetActive(false);
            }
    }
}