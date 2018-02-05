using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField]
    Material secondTexture;

    void OnTriggerEnter(Collider col)
    {
        

        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<MeshRenderer>().material = secondTexture;
        }
    }
}
