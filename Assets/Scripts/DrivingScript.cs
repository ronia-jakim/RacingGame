using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    [SerializeField] private WheelScript [] wheels;

    private float torque = 200;
    private float maxSteerAngle = 30;
    private float maxBrakeTorque = 500;
    private float maxSpeed = 250;

    private Rigidbody rb;

    private float currentSpeed;
    public GameObject BackLights;

    public AudioSource engineSound;
    float rpm;
    public int currentGear = 1;
    public float currentGearPerc;
    public int numGears = 5;
    public float gearLength = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Drive (float accel, float brake, float steer) {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        float thrustTorque = 0;

        if (currentSpeed < maxSpeed) thrustTorque = accel * torque;

        foreach (WheelScript w in wheels) {
            w.wheelCollider.motorTorque = thrustTorque;

            if (w.frontWheel) w.wheelCollider.steerAngle = steer;
            else w.wheelCollider.brakeTorque = brake;

            Quaternion quat;
            Vector3 pos; 
            w.wheelCollider.GetWorldPose(out pos, out quat);

            w.wheel.transform.position = pos;
            w.wheel.transform.rotation = quat;
        }
    }

    public void EngineSound()
    {
        float gearPercentage = (1 / (numGears));

        float targetGearFactor = Mathf.InverseLerp(gearPercentage * currentGear, gearPercentage * (currentGear + 1),
            Mathf.Abs(currentSpeed / maxSpeed));

        // okreslamy ile mocy naszego biegu mamy, (100 procent -> kolejny bieg, 0 -> obnizamy)
        currentGearPerc = Mathf.Lerp(currentGearPerc, targetGearFactor, Time.deltaTime * 5f);

        float gearNumFactor = currentGear / (float)numGears;

        rpm = Mathf.Lerp(gearNumFactor, 1, currentGearPerc);

        float speedPercentage = Mathf.Abs(currentSpeed / maxSpeed);
        float upperGearMax = (1 / (float)numGears) * (currentGear + 1);
        float downGearMax = (1 / (float)numGears) * currentGear;


        // zmiana biegow
        if (currentGear > 0 && speedPercentage < downGearMax)
            currentGear--;

        if (speedPercentage > upperGearMax && (currentGear < (numGears - 1)))
            currentGear++;

        float pitch = Mathf.Lerp(1, 6, rpm);
        engineSound.pitch = Mathf.Min(6, pitch) * 0.1f;
    }
}
