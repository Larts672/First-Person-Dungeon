using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UseScript : MonoBehaviour
{
    public Text useText;
    private bool canUse;

    void Start()
    {
        useText = GameObject.Find("Use Text").GetComponent<Text>();
        useText.rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        useText.rectTransform.position = new Vector3(Screen.width / 2, Screen.height * 0.1f, 0f);
        useText.enabled = false;
    }

    void Update()
    {
        if (canUse && Input.GetKeyDown(KeyCode.E))
        {
            if (this.gameObject.tag == "ExitSphere")
            {
                GameObject.Find("Finish Data").GetComponent<FinishData>().finishType = 1;
                Cursor.lockState = CursorLockMode.None;
                DontDestroyOnLoad(GameObject.Find("Finish Data"));
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                useText.enabled = false;
                canUse = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            useText.enabled = true;
            canUse = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            useText.enabled = false;
            canUse = false;
        }
    }
}
