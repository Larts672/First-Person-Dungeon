using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminsInfo : MonoBehaviour
{
    public Text adminsInfo;
    public GameObject player;
    public GameObject plCamera;

    private string cameraMode;

    void Start()
    {
        adminsInfo = GameObject.Find("Info").GetComponent<Text>();
    }

    void Update()
    {
        if (!player.GetComponent<CameraSwitch>().switched)
        {
            cameraMode = "1st person";
        } else
        {
            cameraMode = "3rd person";
        }

        adminsInfo.text =
            "Camera mode: " + cameraMode + "\n" +
            "Gravity: <color=green>normal</color>" + "\n" +
            "<color=#FF0035><b>--- Position ---</b></color>" + "\n" +
            "x: " + string.Format("{0:0.000}", player.transform.position.x) + "\n" +
            "y: " + string.Format("{0:0.000}", player.transform.position.y) + "\n" +
            "z: " + string.Format("{0:0.000}", player.transform.position.z) + "\n" +
            "<color=#FF0035><b>--- Rotation ---</b></color>" + "\n" +
            "x: " + string.Format("{0:0.000}", plCamera.transform.rotation.x) + "\n" +
            "y: " + string.Format("{0:0.000}", player.transform.rotation.y) + "\n" +
            "<color=#FF0035><b>--- Buttons ---</b></color>" + "\n" + 
            "<color=blue><b>C</b></color> - camera switch <color=red>(not recomended)</color>" + "\n" +
            "<color=blue><b>+/-</b></color> - adding hp";
    }
}
