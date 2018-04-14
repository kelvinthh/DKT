using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sg_Turret : MonoBehaviour {

    public float turnSpeed = 1.0f;  //  The speed at which the turret turns/aims.
    public float inaccuracy = 0.0f;   //  The inaccuracy (in degrees?) of the turret.

    public GameObject primaryAxisObject, secondaryAxisObject;
    public sg_TurretAxis primaryAxisDirection = sg_TurretAxis.y, secondaryAxisDirection = sg_TurretAxis.x;
    public Vector3 adjustPrimary, adjustSecondary;

    public Transform target;

    public bool aim = true;

    private void Start()
    {
        adjustPrimary = primaryAxisObject.transform.localEulerAngles;
        adjustSecondary = secondaryAxisObject.transform.localEulerAngles;
    }

    private void Update()
    {
        if (aim && target) Aim();
    }

    private void Aim()
    {
        if (primaryAxisObject) AimPrimary();
        if (secondaryAxisObject) AimSecondary();
    }

    private void AimPrimary()
    {
        switch (primaryAxisDirection)
        {
            case sg_TurretAxis.x:

                break;
            case sg_TurretAxis.y:
                float currentAngle = primaryAxisObject.transform.eulerAngles.y;
                Vector3 from = primaryAxisObject.transform.position;
                Vector3 to = new Vector3(target.position.x, from.y, target.position.z);
                float angleRad = Mathf.Atan2(from.x - to.x, from.z - to.z);
                float angleDeg = Mathf.Rad2Deg * angleRad;

                Debug.Log("CURRENT ANGLE : " + currentAngle + "      TARGET ANGLE : " + angleDeg);

                if(angleDeg >= 0f)
                {
                    if(Mathf.Abs(currentAngle) >= 90f)
                    {
                        if(Mathf.Abs(currentAngle) >= Mathf.Abs(angleDeg))
                        {
                            primaryAxisObject.transform.localEulerAngles -= new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            primaryAxisObject.transform.localEulerAngles += new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(currentAngle) >= Mathf.Abs(angleDeg))
                        {
                            primaryAxisObject.transform.localEulerAngles -= new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            primaryAxisObject.transform.localEulerAngles += new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(currentAngle) >= 90f)
                    {
                        if (Mathf.Abs(currentAngle) >= Mathf.Abs(angleDeg))
                        {
                            primaryAxisObject.transform.localEulerAngles += new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            primaryAxisObject.transform.localEulerAngles -= new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(currentAngle) >= Mathf.Abs(angleDeg))
                        {
                            primaryAxisObject.transform.localEulerAngles += new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            primaryAxisObject.transform.localEulerAngles -= new Vector3(0, turnSpeed * Time.deltaTime, 0);
                        }
                    }
                }

                //if(angleDeg >= currentAngle + inaccuracy || angleDeg <= currentAngle - inaccuracy)
                //{
                //    Debug.Log("Current Angle : " + currentAngle + "     Target Angle : " + angleDeg);
                //    Debug.DrawLine(from, to, Color.yellow);
                //    if(angleDeg >= 0)
                //    {
                //        //primaryAxisObject.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                //        primaryAxisObject.transform.localEulerAngles -= new Vector3(0, turnSpeed * Time.deltaTime, 0);
                //    }
                //    else
                //    {
                //        //primaryAxisObject.transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
                //        primaryAxisObject.transform.localEulerAngles += new Vector3(0, turnSpeed * Time.deltaTime, 0);
                //    }
                //}
                //else
                //{
                //    Debug.DrawLine(from, to, Color.green);
                //}

                break;
            case sg_TurretAxis.z:

                break;
            default:
                break;
        }
    }
    private void AimSecondary()
    {
        Vector3 targetPosition = secondaryAxisObject.transform.forward;

        switch (secondaryAxisDirection)
        {
            case sg_TurretAxis.x:
                targetPosition = new Vector3(primaryAxisObject.transform.position.x, target.position.y, target.position.z);
                secondaryAxisObject.transform.LookAt(targetPosition, TurretAxisToVector(secondaryAxisDirection));
                float x = secondaryAxisObject.transform.localEulerAngles.x;
                secondaryAxisObject.transform.localEulerAngles = new Vector3(x, adjustSecondary.y, adjustSecondary.z);
                break;
            case sg_TurretAxis.y:
                targetPosition = new Vector3(target.position.x, primaryAxisObject.transform.position.y, target.position.z);
                secondaryAxisObject.transform.LookAt(targetPosition, TurretAxisToVector(secondaryAxisDirection));
                float y = secondaryAxisObject.transform.localEulerAngles.y;
                secondaryAxisObject.transform.localEulerAngles = new Vector3(adjustSecondary.x, y, adjustSecondary.z);
                break;
            case sg_TurretAxis.z:
                targetPosition = new Vector3(target.position.x, target.position.y, primaryAxisObject.transform.position.z);
                secondaryAxisObject.transform.LookAt(targetPosition, TurretAxisToVector(secondaryAxisDirection));
                float z = secondaryAxisObject.transform.localEulerAngles.z;
                secondaryAxisObject.transform.localEulerAngles = new Vector3(adjustSecondary.x, adjustSecondary.y, z);
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