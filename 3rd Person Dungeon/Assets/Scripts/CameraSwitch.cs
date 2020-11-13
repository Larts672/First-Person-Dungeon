using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject firstPersonCamera, thirdPersonCamera;
    public bool switched = false;

    void Start()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("c") && !switched)
        {
            thirdPersonCamera.SetActive(true);
            firstPersonCamera.SetActive(false);
            switched = true;
        } else if (Input.GetKeyDown("c") && switched)
        {
            firstPersonCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
            switched = false;
        }
    }
}
