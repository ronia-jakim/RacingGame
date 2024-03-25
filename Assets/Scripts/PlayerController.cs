using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript driveScript;

    float lastTimeMoved = 0f;
    CheckPointController checkController;

    void Start()
    {
        driveScript = GetComponent<DrivingScript>();
        checkController = driveScript.rb.GetComponent<CheckPointController>();
    }

    void Update()
    {
        if (checkController.laps == RaceController.totalLaps + 1) return;

        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");

        if (driveScript.rb.velocity.magnitude > 1 && RaceController.racing) lastTimeMoved = Time.time;

        if ((Time.time > lastTimeMoved + 15 || driveScript.rb.gameObject.transform.position.y < -5) && RaceController.racing) {
            driveScript.rb.transform.position = checkController.lastPoint.transform.position + Vector3.up * 2;

            driveScript.rb.transform.rotation = checkController.lastPoint.transform.rotation;

            driveScript.rb.gameObject.layer = 6;
            Invoke("ResetLayer", 3);
        }

        if (!RaceController.racing) accel = 0;

        driveScript.Drive(accel, brake, steer);
    }

    void ResetLayer() {
        driveScript.rb.gameObject.layer = 0;
    }
}
