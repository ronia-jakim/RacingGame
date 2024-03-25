using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int laps = 0;
    public int checkPoints = -1;
    int pointCount;
    public int nextPoint;

    public GameObject lastPoint;

    void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        pointCount = checkpoints.Length;

        for (int i = 0; i < pointCount; i++) {
            if (checkpoints[i].name == "0") {
                lastPoint = checkpoints[i];
                break;
            }
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "CheckPoint") {
            int thisPoint = int.Parse(other.gameObject.name);

            if (thisPoint == nextPoint) {
                checkPoints = thisPoint;
                lastPoint = other.gameObject;

                if (checkPoints == 0) {
                    laps++;
                    Debug.Log("Lap: " + laps);
                }
                Debug.Log(this.name + pointCount);
                nextPoint = (nextPoint + 1) % pointCount;
            }
        }
    }
}
