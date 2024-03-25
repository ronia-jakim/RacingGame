using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ColorCar : MonoBehaviour
{
    public Renderer rend;

    public Slider redS;
    public Slider greenS;
    public Slider blueS;

    public Color col;

    public static Color IntToColor(int red, int green, int blue) {
        float r = (float)red /255;
        float g = (float)green /255;
        float b = (float)blue /255;

        Color c = new Color (r, g, b);
        return c;
    }

    public void SetCarColor(int red, int green, int blue) {
        col = IntToColor(red, green, blue);
        rend.material.color = col;

        PlayerPrefs.SetInt("Red", red);
        // PlayerPrefs.GetInt("Red");
        // PlayerPrefs.HasKey("Red") <- czy klucz "Red" istnieje
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", blue);
    }

    void Start () {
        col = IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
        rend.material.color = col;

        redS.value = (int)(col.r * 255);
        greenS.value = (int)(col.g * 255);
        blueS.value = (int)(col.b * 255);
    }

    void Update() {
        SetCarColor((int)redS.value, (int)greenS.value, (int)blueS.value);
    }
}
