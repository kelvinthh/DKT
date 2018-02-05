using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateScript : MonoBehaviour
{

    public GameObject cameraObj;
    public GameObject capsuleObj;
    public GameObject[] UI;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xVal;
        xVal = cameraObj.transform.eulerAngles.x;
        //Debug.Log(xVal);

        if (xVal < 20)
        {
            //Set position to player position 
            transform.position = capsuleObj.transform.position;
            //set rotation to be the same as players Y rotation
            transform.rotation = Quaternion.Euler(0, cameraObj.transform.rotation.eulerAngles.y, 0);

            foreach (GameObject wi in UI)
            {
                wi.SetActive(true);
            }
        }
        if (xVal < 10)
        {
            foreach (GameObject wi in UI)
            {
                wi.SetActive(false);
            }
        }
    }
}