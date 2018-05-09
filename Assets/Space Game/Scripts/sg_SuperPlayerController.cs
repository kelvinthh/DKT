using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class sg_SuperPlayerController : MonoBehaviour
{
    public GameObject playerTarget;
    [SerializeField]
    private Transform m_transform;
    private sg_ShipAi m_playerShipAi;

    public float areaRadius = 10f;
    public float areaHeight = 10f;

    public PlayerControlMode mode;

    [Header("")]
    public bool debug = false;
    private int verticalLines = 30;
    private int horizontalLines = 5;

    public GameObject targetObject;
    public LayerMask targetMask;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (playerTarget)
        {
            switch (mode)
            {
                case PlayerControlMode.Cylinder:
                    playerTarget.transform.position = RadiusTools.CalculatePosOnCylinder(gameObject.transform.forward * 10, m_transform.position, areaRadius, areaHeight);
                    if (debug) RadiusTools.DebugCylinder(m_transform.position, areaRadius, areaHeight, verticalLines, horizontalLines);
                    break;
                case PlayerControlMode.Plane:
                    break;
                case PlayerControlMode.Sphere:
                    break;
                default:
                    break;
            }
        }

        Target();
    }

    private void Target()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_transform.position, m_transform.forward, out hit, Mathf.Infinity, targetMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.GetComponent<sg_ShipAi>())
            {
                if (hitObject != targetObject)
                {
                    if (targetObject) targetObject.GetComponent<sg_ShipAi>().Deselect();
                    targetObject = hitObject;
                    targetObject.GetComponent<sg_ShipAi>().Select();
                }
                Debug.DrawLine(m_transform.position, hitObject.transform.position, Color.green);
            }
            else
            {
                if (targetObject) targetObject.GetComponent<sg_ShipAi>().Deselect();
                targetObject = null;
            }
        }
        else
        {
            if (targetObject) targetObject.GetComponent<sg_ShipAi>().Deselect();
            targetObject = null;
        }
    }

    public void SetPlayerShip(sg_ShipAi ai)
    {
        m_playerShipAi = ai;
    }
}

public enum PlayerControlMode
{
    Cylinder,
    Plane,
    Sphere
}

public static class RadiusTools
{
    public static Vector3 FindSpawnPosition(float minRadius, float maxRadius, float height)
    {
        Vector3 result = Vector3.zero;
        float x = 0, y = 0, z = 0;
        while (Vector3.Magnitude(result) < minRadius || Vector3.Magnitude(result) > maxRadius)
        {
            x = Random.Range(-maxRadius, maxRadius);
            z = Random.Range(-maxRadius, maxRadius);
            result = new Vector3(x, 0, z);
        }
        y = Random.Range(-height, height);
        result.y = y;
        return result;
    }

    public static Vector3 CalculatePosOnCylinder(Vector3 input, Vector3 origin, float radius, float height)
    {
        Vector3 result = input;
        input = input - origin;
        Vector3 xz = new Vector3(input.x, 0f, input.z);
        Vector3 xyz = xz.normalized * radius;
        xyz.y = Mathf.Clamp(input.y, -height, height);
        result = xyz;

        return result;
    }

    public static void DebugCylinder(Vector3 origin, float radius, float height, int verticalLines, int horizontalLines)
    {
        float deltaTheta = (2f * Mathf.PI) / verticalLines;
        float theta = 0f;

        Vector3 oldPos = new Vector3(radius * Mathf.Cos(theta - deltaTheta), height, radius * Mathf.Sin(theta - deltaTheta));

        for (int i = 0; i < verticalLines; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            float y = radius;
            Debug.DrawLine(new Vector3(x, y, z) + origin, new Vector3(x, -y, z) + origin, Color.magenta);
            float hY = y;
            for (int j = -horizontalLines; j < horizontalLines + 1; j++)
            {
                hY = (height / horizontalLines) * j;
                Debug.DrawLine(new Vector3(oldPos.x, hY, oldPos.z) + origin, new Vector3(x, hY, z) + origin, Color.cyan);
            }

            oldPos = new Vector3(x, y, z);

            theta += deltaTheta;
        }
    }
}