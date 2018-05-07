using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_LerpRotation : MonoBehaviour {

    public Transform followTransform;
    public float lerpSpeed = 10.0f;
    public bool doFollow = true;
    private Transform m_transform;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(followTransform && m_transform && doFollow)
        {
            Quaternion a = m_transform.rotation;
            Quaternion b = followTransform.rotation;
            m_transform.rotation = Quaternion.Lerp(a, b, lerpSpeed * Time.deltaTime);
        }
    }
}
