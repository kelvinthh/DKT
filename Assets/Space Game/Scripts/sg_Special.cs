using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class sg_Special : MonoBehaviour {

    public int value;
    public sg_SpecialType type;
    private sg_SpecialType m_prevType;
    public Sprite healthSprite, damageSprite, shieldSprite;

    private SpriteRenderer m_renderer;

    public float rotateSpeed = 50f;

    private Transform m_chiild;

    private void Start()
    {
        m_chiild = transform.GetChild(0).GetComponent<Transform>();
        m_renderer = m_chiild.GetComponent<SpriteRenderer>();
        SetImage();
    }

    private void SetImage()
    {
        switch (type)
        {
            case sg_SpecialType.Health:
                m_renderer.sprite = healthSprite;
                break;
            case sg_SpecialType.Damage:
                m_renderer.sprite = damageSprite;
                break;
            case sg_SpecialType.Shield:
                m_renderer.sprite = shieldSprite;
                break;
            default:
                break;
        }

        m_prevType = type;
    }

    private void Update()
    {
        m_chiild.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (m_prevType != type) SetImage();
    }
}

public enum sg_SpecialType
{
    Health, Damage, Shield
}