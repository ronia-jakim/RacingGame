using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public CheckPointController [] carsController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("--------------------------------------------");
        InvokeRepeating("CountDown", 3, 1);

        GameObject [] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length]; 
        for (int i = 0; i < cars.Length; i++) {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    void LateUpdate()
    {
        int finishedLap = 0;
        foreach (CheckPointController c in carsController) {
            if (c.laps == totalLaps + 1) finishedLap++;

            if (finishedLap == carsController.Length && racing) {
                racing = false;
                Debug.Log("Finish Race");
            }
        }
    }

    void CountDown () {
        if (timer > 0) {
            Debug.Log("Rozpoczęcie wyścigu za: " + timer);
            timer--;
        }
        else {
            Debug.Log("Start!");
            racing = true;
            CancelInvoke("CountDown");
        }
    }
}
