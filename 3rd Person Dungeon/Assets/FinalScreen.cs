using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
