using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Button restartRunButton;
    public Button menuButton;

    // Update is called once per frame
    void Start()
    {
        //Restart Run
        restartRunButton.onClick.AddListener(BackToStart);
        
        //Menu
        menuButton.onClick.AddListener(GoToMenu);
    }
    
    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void BackToStart()
    {
        SceneManager.LoadScene("Level1");
    }
}
