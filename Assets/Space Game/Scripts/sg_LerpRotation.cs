using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_LerpRotation : MonoBehaviour {

    public Transform followTransform;
    public float lerpSpeed = 10.0f;
    public bool doFollow = true;
    private Transform m_transform;
    public sg_RotationFollowType followType;

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

            switch (followType)
            {
                case sg_RotationFollowType.lerp:
                    m_transform.rotation = Quaternion.Lerp(a, b, lerpSpeed * Time.deltaTime);
                    break;

                case sg_RotationFollowType.slerp:
                    m_transform.rotation = Quaternion.Slerp(a, b, lerpSpeed * Time.deltaTime);
                    break;

                default:
                    m_transform.rotation = Quaternion.Lerp(a, b, lerpSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    public enum sg_RotationFollowType { lerp, slerp }
}
