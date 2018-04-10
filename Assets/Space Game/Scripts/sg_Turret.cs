using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Turret : MonoBehaviour {

    public float turnSpeed = 1.0f;  //  The speed at which the turret turns/aims.
    public float inaccuracy = 0.0f;   //  The inaccuracy (in degrees?) of the turret.

    public GameObject primaryAxisObject, secondaryAxisObject;
    public sg_TurretAxis primaryAxisDirection = sg_TurretAxis.y, secondaryAxisDirection = sg_TurretAxis.x;
    public Vector3 enforcedPrimary, enforcedSecondary;

    public Transform target;

    public bool aim = true;

    private void Start()
    {
        enforcedPrimary = primaryAxisObject.transform.localEulerAngles;
        enforcedSecondary = secondaryAxisObject.transform.localEulerAngles;
    }

    private void Update()
    {
        if (aim && target) Aim();
    }

    private void Aim()
    {
        if (primaryAxisObject) AimPrimary();
        if(secondaryAxisObject) AimSecondary();

        Debug.DrawLine(secondaryAxisObject.transform.position, target.transform.position, Color.yellow);
    }

    private void AimPrimary()
    {
        Vector3 targetPosition = primaryAxisObject.transform.forward;

        switch (primaryAxisDirection)
        {
            case sg_TurretAxis.x:
                targetPosition = new Vector3(primaryAxisObject.transform.position.x, target.position.y, target.position.z);
                float x = primaryAxisObject.transform.localEulerAngles.x;
                primaryAxisObject.transform.localEulerAngles = new Vector3(x, enforcedPrimary.y, enforcedPrimary.z);
                break;
            case sg_TurretAxis.y:
                targetPosition = new Vector3(target.position.x, primaryAxisObject.transform.position.y, target.position.z);
                float y = primaryAxisObject.transform.localEulerAngles.y;
                primaryAxisObject.transform.localEulerAngles = new Vector3(enforcedPrimary.x, y, enforcedPrimary.z);
                break;
            case sg_TurretAxis.z:
                targetPosition = new Vector3(target.position.x, target.position.y, primaryAxisObject.transform.position.z);
                float z = primaryAxisObject.transform.localEulerAngles.z;
                primaryAxisObject.transform.localEulerAngles = new Vector3(enforcedPrimary.x, enforcedPrimary.y, z);
                break;
            default:
                break;
        }

        primaryAxisObject.transform.LookAt(targetPosition, TurretAxisToVector(primaryAxisDirection));
    }
    private void AimSecondary()
    {
        Vector3 targetPosition = secondaryAxisObject.transform.forward;

        switch (secondaryAxisDirection)
        {
            case sg_TurretAxis.x:
                targetPosition = new Vector3(secondaryAxisObject.transform.position.x, target.position.y, target.position.z);
                secondaryAxisObject.transform.LookAt(targetPosition, TurretAxisToVector(secondaryAxisDirection));
                float x = secondaryAxisObject.transform.localEulerAngles.x;
                secondaryAxisObject.transform.localEulerAngles = new Vector3(x, enforcedSecondary.y, enforcedSecondary.z);
                break;
            case sg_TurretAxis.y:
                targetPosition = new Vector3(target.position.x, secondaryAxisObject.transform.position.y, target.position.z);
                float y = secondaryAxisObject.transform.localEulerAngles.y;
                secondaryAxisObject.transform.localEulerAngles = new Vector3(enforcedSecondary.x, y, enforcedSecondary.z);
                break;
            case sg_TurretAxis.z:
                targetPosition = new Vector3(target.position.x, target.position.y, secondaryAxisObject.transform.position.z);
                float z = secondaryAxisObject.transform.localEulerAngles.z;
                secondaryAxisObject.transform.localEulerAngles = new Vector3(enforcedSecondary.x, enforcedSecondary.y, z);
                break;
            default:
                break;
        }
    }

    private Vector3 TurretAxisToVector(sg_TurretAxis axis)
    {
        Vector3 result = Vector3.zero;
        switch (axis)
        {
            case sg_TurretAxis.x:
                result = Vector3.right;
                break;
            case sg_TurretAxis.y:
                result = Vector3.up;
                break;
            case sg_TurretAxis.z:
                result = Vector3.forward;
                break;
            default:
                break;
        }
        return result;
    }
}

public enum sg_TurretAxis
{
    x,y,z
}