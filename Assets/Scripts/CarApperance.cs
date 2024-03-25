using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CarApperance : MonoBehaviourPunCallbacks
{
    public string PlayerName;
    public Color CarColor;
    public Text NameText;
    public Renderer CarRenderer;

/*    public GameObject carPrefab;
    public Transform[] spawnPoints;

    public int playerCount;*/

    public int playerNumber;

    public void SetNameAndColor (string name, Color color) {
        NameText.text = name;
        CarRenderer.material.color = color;
        NameText.color = color;
    }

    public Camera backCamera;
    public void SetLocalPlayer () {
        FindObjectOfType<CameraController>().SetCameraProperties(this.gameObject);
        PlayerName = PlayerPrefs.GetString("PlayerName");

        CarColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));

        NameText.text = PlayerName;
        CarRenderer.material.color = CarColor;
        NameText.color = CarColor;

        RenderTexture rt = new RenderTexture(1024, 1024, 0); 
        backCamera.targetTexture = rt;
        FindObjectOfType<RaceController>().SetMirror(backCamera);
    }


    // void Start()
    // {
    //     if (playerNumber == 0) {
    //         PlayerName = PlayerPrefs.GetString("PlayerName");
    //         CarColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
    //     }
    //     else {
    //         PlayerName = "Random" + playerNumber;
    //         CarColor = new Color (Random.Range(0f, 255f) /255, Random.Range(0f, 255f) /255, Random.Range(0f, 255f) /255);
    //     }
    //     NameText.text = PlayerName;
    //     CarRenderer.material.color = CarColor;
    //     NameText.color = CarColor;
    // }


    void Update()
    {
        
    }
}
