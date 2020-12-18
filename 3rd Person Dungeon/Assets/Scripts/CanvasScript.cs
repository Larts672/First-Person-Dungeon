using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasScript : MonoBehaviour
{
    private Button _exitButton;
    private Button _restartButton;
    private Text finalText;

    void Start()
    {
        finalText = GameObject.Find("Final Text").GetComponent<Text>();
        _exitButton = GameObject.Find("Exit Button").GetComponent<Button>();
        _restartButton = GameObject.Find("Restart Button").GetComponent<Button>();

        _exitButton.onClick.AddListener(Exit);
        _restartButton.onClick.AddListener(Restart);

        if (GameObject.Find("Finish Data").GetComponent<FinishData>().finishType == 0)
        {
            finalText.text = "You were slain.";
        } else if (GameObject.Find("Finish Data").GetComponent<FinishData>().finishType == 1)
        {
            finalText.text = "You won!";
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Restart() 
    {
        SceneManager.LoadScene(0);
    }
}
