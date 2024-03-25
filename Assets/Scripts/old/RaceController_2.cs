using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;

public class RaceController_2 : MonoBehaviourPunCallbacks
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public GameObject startRace;
    public GameObject waitingText;

    public CheckPointController [] carsController;

    public Text StartText;
    public AudioClip CountSound;
    public AudioClip StartSound;
    public AudioSource audioSource;

    public GameObject EndPanel;

    public GameObject carPrefab;
    public Transform[] spawnPoints;
    public int playerCount;

    [PunRPC]
    public void StartGame () {
        InvokeRepeating("CountDown", 3, 1);
        startRace.SetActive(false);
        waitingText.SetActive(false);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for (int i = 0; i < cars.Length; i++) {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    //public PhotonView photonView;

    public void BeginGame () {
        if (PhotonNetwork.IsMasterClient) {
            photonView.RPC("StartGame", RpcTarget.All, null);
        }
    }

    void Awake () {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Update() {
        //Debug.Log(PhotonNetwork.IsConnected);
    }
    void Start()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        EndPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        StartText.gameObject.SetActive(false);
        startRace.SetActive(false);
        waitingText.SetActive(false);

        int RandomPos = Random.Range(0, spawnPoints.Length);
        Vector3 startPos = spawnPoints[RandomPos].position;
        Quaternion startRot = spawnPoints[RandomPos].rotation;

        GameObject playerCar = null;
        if (PhotonNetwork.IsConnected) {
            startPos = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
            startRot = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;

            if (OnlinePlayer.LocalPlayerInstance == null) {
                playerCar = PhotonNetwork.Instantiate(carPrefab.name, startPos, startRot, 0);
            }

            if (PhotonNetwork.IsMasterClient) {
                startRace.SetActive(true);
            }
            else {
                waitingText.SetActive(true);
            }
        }

        playerCar.GetComponent<DrivingScript>().enabled = true;
        playerCar.GetComponent<PlayerController>().enabled = true;
    }

    void LateUpdate()
    {
        int finishedLap = 0;
        foreach (CheckPointController c in carsController) {
            if (c.laps == totalLaps + 1) finishedLap++;

            if (finishedLap == carsController.Length && racing) {
                racing = false;
                //Debug.Log("Finish Race");
                EndPanel.SetActive(true);
            }
        }
    }

    void CountDown () {
        StartText.gameObject.SetActive(true);

        if (timer > 0) {
            //Debug.Log("Rozpoczęcie wyścigu za: " + timer);
            StartText.color = Color.yellow;
            StartText.text = "Rozpoczęcie wyścigu za: " + timer;
            audioSource.PlayOneShot(CountSound);
            timer--;
        }
        else {
            //Debug.Log("Start!");
            StartText.color = Color.green;
            StartText.text = "Start!!!!";
            racing = true;
            CancelInvoke("CountDown");
            Invoke(nameof(HideStartText), 1);
            audioSource.PlayOneShot(StartSound);
        }
    }

    void HideStartText()
    {
        StartText.gameObject.SetActive(false);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}