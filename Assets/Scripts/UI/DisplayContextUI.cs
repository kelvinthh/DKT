using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayContextUI : MonoBehaviour
{

    private Camera cameraObj;
    private Transform transform;

    [SerializeField] private GameObject menuButton;

    [Tooltip("Degrees that the player has to look below the horizon before the context menu is shown.")]
    [SerializeField] private float displayAngle = 50;

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
            

            if (CheckAngle() > displayAngle && CheckAngle() < 100) //evaluate the cameras current X value to be below a certain threshold...
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
        transform.rotation = Quaternion.Euler(0, cameraObj.transform.rotation.eulerAngles.y, 0);
        
        menuButton.SetActive(true);

    }
    void HideUI()
    {
        menuButton.SetActive(false);

    }
}