using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    byte maxPlayerPerRoom = 10;
    bool isConnecting;
    public Text networkText;
    string gameVersion = "1";

    public GameObject button;


    public InputField playerName;
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PlayerPrefs.HasKey("PlayerName")) playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    void Start () {
        networkText.text = "";
        button.SetActive(false);
        isConnecting = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        networkText.text += "Connected to master\n";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        button.SetActive(true);
        networkText.text += "Connected to lobby\n";
    }

    public override void OnJoinedRoom () {
        networkText.text += "Joined room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players\n";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        PhotonNetwork.LoadLevel("TestTrack");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join room because " + message + " \n";
    }

    public void ConnectedNetwork () {
        networkText.text = "";
        // PhotonNetwork.NickName = playerName.text;

        PhotonNetwork.JoinOrCreateRoom("test", new RoomOptions() {MaxPlayers = maxPlayerPerRoom, IsVisible = true, IsOpen = true}, TypedLobby.Default, null);
    }


    public void SetName (string name) {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void StartTrial() {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(isConnecting) Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
