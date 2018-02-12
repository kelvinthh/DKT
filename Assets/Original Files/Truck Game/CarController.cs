using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CarController : MonoBehaviour {

    [SerializeField]
    private List<Axle> axles;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float maxSteering; 
	
	// Update is called once per frame
	void FixedUpdate () {
        float speed = maxSpeed * Input.GetAxis("Vertical");
        float steer = maxSteering * Input.GetAxis("Horizontal");

        if (Input.GetButton("Fire1"))
        {
            Application.LoadLevel("Main");
        }


        foreach (Axle wheels in axles)
        {
            if (wheels.turning)
            {
                wheels.leftWheel.steerAngle = steer;
                wheels.rightWheel.steerAngle = steer;
            }
            if (wheels.drive)
            {
                wheels.leftWheel.motorTorque = speed;
                wheels.rightWheel.motorTorque = speed;
            }
        }
	}
}

[System.Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool drive;
    public bool turning;
}
