using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Rotate : MonoBehaviour {

    [SerializeField]
    float speed;



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
