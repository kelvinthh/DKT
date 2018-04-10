using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Turret : MonoBehaviour {

    public float turnSpeed = 1.0f;  //  The speed at which the turret turns/aims.
    public float inaccuracy = 0.0f;   //  The inaccuracy (in degrees?) of the turret.

    public GameObject primaryAxisObject, secondaryAxisObject;
    public sg_TurretAxis primaryAxisDirection = sg_TurretAxis.y, secondaryAxisDirection = sg_TurretAxis.x;

    public Transform target;

    public bool aim = true;

    private void Update()
    {
        if (aim && target) Aim();
    }

    private void Aim()
    {
        if (primaryAxisObject) AimPrimary();
        if(secondaryAxisObject) AimSecondary();
    }

    private void AimPrimary()
    {
        Vector3 targetPosition = primaryAxisObject.transform.forward;

        switch (primaryAxisDirection)
        {
            case sg_TurretAxis.x:
                targetPosition = new Vector3(primaryAxisObject.transform.position.x, target.position.y, target.position.z);
                break;
            case sg_TurretAxis.y:
                targetPosition = new Vector3(target.position.x, primaryAxisObject.transform.position.y, target.position.z);
                break;
            case sg_TurretAxis.z:
                targetPosition = new Vector3(target.position.x, target.position.y, primaryAxisObject.transform.position.z);
                break;
            default:
                break;
        }

        primaryAxisObject.transform.LookAt(targetPosition);
    }
    private void AimSecondary()
    {
        Vector3 targetPosition = secondaryAxisObject.transform.forward;

        switch (secondaryAxisDirection)
        {
            case sg_TurretAxis.x:
                targetPosition = new Vector3(secondaryAxisObject.transform.position.x, target.position.y, target.position.z);
                break;
            case sg_TurretAxis.y:
                targetPosition = new Vector3(target.position.x, secondaryAxisObject.transform.position.y, target.position.z);
                break;
            case sg_TurretAxis.z:
                targetPosition = new Vector3(target.position.x, target.position.y, secondaryAxisObject.transform.position.z);
                break;
            default:
                break;
        }

        secondaryAxisObject.transform.LookAt(targetPosition);
    }
}

public enum sg_TurretAxis
{
    x,y,z
}