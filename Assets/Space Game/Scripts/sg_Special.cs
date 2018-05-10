using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sg_Special : MonoBehaviour {

    public int value;
    public sg_SpecialType type;
    public Sprite healthSprite, damageSprite, shieldSprite;

    public Image myImage;

    public float rotateSpeed = 0.5f;

    private Transform m_transform;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        SetImage();
    }

    private void SetImage()
    {
        switch (type)
        {
            case sg_SpecialType.Health:
                myImage.sprite = healthSprite;
                break;
            case sg_SpecialType.Damage:
                myImage.sprite = damageSprite;
                break;
            case sg_SpecialType.Shield:
                myImage.sprite = shieldSprite;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        m_transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}

public enum sg_SpecialType
{
    Health, Damage, Shield
}