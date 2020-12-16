using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasScript : MonoBehaviour
{
    public Button _exitButton;
    public Button _restartButton;
    void Start()
    {
        _exitButton.onClick.AddListener(Exit);
        _restartButton.onClick.AddListener(Restart);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Restart() 
    {
        SceneManager.LoadScene(1);
    }
}
