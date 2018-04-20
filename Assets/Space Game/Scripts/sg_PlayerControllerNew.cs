using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class sg_PlayerControllerNew : MonoBehaviour
{
    public GameObject playerTarget;
    private Transform m_transform;

    public float areaRadius = 10f;
    public float areaHeight = 10f;

    [Header("")]
    public bool debug = false;
    private int verticalLines = 30;
    private int horizontalLines = 5;

    private void Start()
    {
        m_transform = transform;
    }

    private void Update()
    {
        if(playerTarget) playerTarget.transform.position = CalculatePosition(gameObject.transform.forward * 10);

        if (debug) DebugLines();
    }

    public Vector3 CalculatePosition(Vector3 input)
    {
        Vector3 result = input;
        input = input - m_transform.position;
        Vector3 xz = new Vector3(input.x, 0f, input.z);
        Vector3 xyz = xz.normalized * areaRadius;
        xyz.y = Mathf.Clamp(input.y, -areaHeight, areaHeight);
        result = xyz;

        Debug.DrawLine(m_transform.position, result, Color.yellow);

        return result;
    }

    private void DebugLines()
    {
        Vector3 origin = m_transform.position;

        float deltaTheta = (2f * Mathf.PI) / verticalLines;
        float theta = 0f;

        Vector3 oldPos = new Vector3(areaRadius * Mathf.Cos(theta - deltaTheta), areaHeight, areaRadius * Mathf.Sin(theta - deltaTheta));

        for(int i = 0; i < verticalLines; i++)
        {
            float x = areaRadius * Mathf.Cos(theta);
            float z = areaRadius * Mathf.Sin(theta);
            float y = areaHeight;
            Debug.DrawLine(new Vector3(x,y,z) + origin, new Vector3(x,-y,z) + origin, Color.magenta);
            float hY = y;
            for(int j = -horizontalLines; j < horizontalLines + 1; j++)
            {
                hY = (areaHeight / horizontalLines) * j;

                Debug.DrawLine(new Vector3(oldPos.x, hY, oldPos.z) + origin, new Vector3(x, hY, z) + origin, Color.cyan);
            }

            oldPos = new Vector3(x, y, z);

            theta += deltaTheta;
        }
    }
}