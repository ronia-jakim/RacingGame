using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int laps = 0;
    public int checkPoints = -1;
    int pointCount;
    public int nextPoint;

    void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        pointCount = checkpoints.Length;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "CheckPoint") {
            int thisPoint = int.Parse(other.gameObject.name);

            if (thisPoint == nextPoint) {
                checkPoints = thisPoint;

                if (checkPoints == 0) {
                    laps++;
                    Debug.Log("Lap: " + laps);
                }

                nextPoint = (nextPoint + 1) % pointCount;
            }
        }
    }
}
