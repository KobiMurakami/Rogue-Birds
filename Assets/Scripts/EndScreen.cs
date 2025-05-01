using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject restartRunButton;
    public GameObject menuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartRunButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //restartRunButton.onClick.AddListener(BackToStart);
        
        //Menu
        //menuButton.onClick.AddListener(GoToMenu);
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
