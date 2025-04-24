using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public string nextLevelName;
    public string levelName;
    
    //Win and Lose screens for the game 
    public GameObject winText;
    public GameObject loseText;
    
    //Buttons
    public Button nextLevelButton;
    public Button replayButtonWin;
    public Button replayButtonLose;
    public Button menuButtonWin;
    public Button menuButtonLose;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
    }
    private void OnEnable()
    {
        //enemies = FindObjectsByType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (EnemiesAreDead())
        {
            GoNextLevel();
        }*/
        
        //Debug code for testing win and loss screens
        if (Input.GetKeyDown(KeyCode.W))
        {
            WinScreen();
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseScreen();
        }
    }

    /*bool EnemiesAreDead()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }*/

    void GoNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void WinScreen()
    {
        winText.SetActive(true);

        //Logic for going to the next stage, returning to the main menu, or replaying the current level
        //TODO: Fix problem where it just instantly reloads regardless of input
        if (nextLevelButton)
        {
            GoNextLevel();
        }

        if (replayButtonWin)
        {
            ReloadLevel();
        }

        //Menu
        if (menuButtonWin)
        {
            GoToMenu();
        }
    }
    
    void LoseScreen()
    {
        loseText.SetActive(true);

        if (replayButtonLose)
        {
            ReloadLevel();
        }

        //Menu WIP
        /*if (menuButtonLose)
        {
            //SceneManager.LoadScene(menu);
        }*/
    }
}
