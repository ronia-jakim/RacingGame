using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public Vector3[] position;
    int activePosition = 0;

    void Start () {
        if (position.Length == 0) return;

        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = position[activePosition];
    }

    void Update () {
        if (position.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.T)) {
            activePosition = (activePosition + 1) % position.Length;
            cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = position[activePosition];
        }
    }

    public void SetCameraProperties (GameObject car) {
        cam.Follow = car.GetComponent<DrivingScript>().rb.transform;
        cam.LookAt = car.GetComponent<DrivingScript>().cameraTarget.transform;
    }
}
