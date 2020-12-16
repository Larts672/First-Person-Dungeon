using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    public Text finalText;

    void Start()
    {
        finalText = GameObject.Find("FinalText").GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
