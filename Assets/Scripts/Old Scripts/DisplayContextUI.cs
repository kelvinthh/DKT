using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayContextUI : MonoBehaviour
{

    private Camera cameraObj;
    private Transform transform;

    [SerializeField] private GameObject menuButton;

    private float displayAngle = 40;

    private bool displayed = false;

    private void Start()
    {
        cameraObj = Camera.main;
        transform = GetComponent<Transform>();
        menuButton.SetActive(false);
    }

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
        menuButton.SetActive(true);

    }
    void HideUI()
    {
        menuButton.SetActive(false);

    }
}