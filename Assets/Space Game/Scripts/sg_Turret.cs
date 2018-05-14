using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Turret : MonoBehaviour {

    public float turnSpeed = 1.0f;  //  The speed at which the turret turns/aims.
    public float inaccuracy = 0.0f;   //  The inaccuracy (in degrees?) of the turret.

    public Transform primaryAxisObject, secondaryAxisObject;
    private Quaternion adjustPrimary, adjustSecondary;
    public float yLimit = 90f;
    public float xLimit = 90f;

    public GameObject targetObject;
    private Vector3 target;

    public bool aim = true;

    private void Start()
    {
        adjustPrimary = primaryAxisObject.transform.localRotation;
        adjustSecondary = secondaryAxisObject.transform.localRotation;
    }

    private void Update()
    {
        if (aim) Aim();
    }

    private void Aim()
    {
        if (targetObject) target = targetObject.transform.position;

        float difference = 0.0f;

        Vector3 targetPosition = Vector3.zero;
        Quaternion targetRotation = Quaternion.identity;

        if (primaryAxisObject && (yLimit != 0f))
        {
            targetPosition = primaryAxisObject.InverseTransformPoint(target);
            difference = Mathf.Atan2(targetPosition.x, targetPosition.z) * Mathf.Rad2Deg;
            if (difference >= 180f) difference = 180f - difference;
            if (difference <= -180f) difference = -180f + difference;
            targetRotation = primaryAxisObject.rotation * Quaternion.Euler(0f, Mathf.Clamp(difference, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime), 0f);
            if ((yLimit < 360f) && (yLimit > 0f)) primaryAxisObject.rotation = Quaternion.RotateTowards(primaryAxisObject.parent.rotation * adjustPrimary, targetRotation, yLimit);
            else primaryAxisObject.rotation = targetRotation;
        }

        if (secondaryAxisObject && (xLimit != 0f))
        {
            targetPosition = secondaryAxisObject.InverseTransformPoint(target);
            difference = -Mathf.Atan2(targetPosition.y, targetPosition.z) * Mathf.Rad2Deg;
            if (difference >= 180f) difference = 180f - difference;
            if (difference <= -180f) difference = -180f + difference;
            targetRotation = secondaryAxisObject.rotation * Quaternion.Euler(Mathf.Clamp(difference, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime), 0f, 0f);
            if ((xLimit < 360f) && (xLimit > 0f)) secondaryAxisObject.rotation = Quaternion.RotateTowards(secondaryAxisObject.parent.rotation * adjustSecondary, targetRotation, xLimit);
            else secondaryAxisObject.rotation = targetRotation;
        }
    }
}