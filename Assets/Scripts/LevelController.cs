using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public string nextLevelName;
    public string levelName;
    public int currentLevelNumber;

    public Scoring scoreManager;
    public bool loseCondition = false;
    public bool calledOnce = false;
    
    //Win and Lose screens for the game 
    public GameObject winText;
    public GameObject loseText;
    
    //Buttons
    public Button nextLevelButton;
    public Button replayButtonWin;
    public Button replayButtonLose;
    public Button menuButtonWin;
    public Button menuButtonLose;

    //public GameObject Enemy();
    //public GameObject Boss();
    
    //Level Finished Event
    public delegate void LevelFinished();
    public static event LevelFinished OnLevelFinished;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        scoreManager = GetComponent<Scoring>();
        SlingShot.OnLevelFail += LevelOnLevelFail;
    }
    private void OnEnable()
    {
        //enemies = FindObjectsByType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindWithTag("Enemy"); //find gameobjects with the tag "Enemy"

        if (enemies == null)
        {
            Debug.Log("Win Condition Triggered");
            int currentScore = scoreManager.GetComponent<Scoring>().score;
            PlayerPrefs.SetInt("currentscore", currentScore);
            Debug.Log("Sending Updated Score to next level");
            WinScreen();
        }

        if(loseCondition && !calledOnce) {
            calledOnce = true;
            Debug.Log("Lose Condition Triggered");

            int highScore = PlayerPrefs.GetInt("highscore", 0);
            int currentScore = scoreManager.GetComponent<Scoring>().score;
            if(currentScore >= highScore) {
                PlayerPrefs.SetInt("highscore", currentScore);
            }
            if(currentLevelNumber >= PlayerPrefs.GetInt("highestlevel", 0)) {
                PlayerPrefs.SetInt("highestlevel", currentLevelNumber);
            }

            int cumScore = PlayerPrefs.GetInt("cumscore", 0);
            PlayerPrefs.SetInt("cumscore", (cumScore + currentScore));
            Debug.Log("Game Lost, Setting current score to 0");
            PlayerPrefs.SetInt("currentscore", 0);

            LoseScreen();
        }

        // Debug code for testing win and loss screens
        // if (scoreManager.numEnemiesInLevel == scoreManager.numEnemiesKilled)
        // {
        //     winText.SetActive(true);
        //     if(Input.GetKeyDown(KeyCode.W))
        //     {
        //         GoNextLevel();
        //     }
        // }
        // if(loseCondition)
        // {
        //     loseText.SetActive(true);
        //     if(Input.GetKeyDown(KeyCode.R))
        //     {
        //         ReloadLevel();
        //     }
        // }
        
        
    }

    public void GoNextLevel()
    {
        BirdBagManager.Instance.ResetBag();
        
        StartCoroutine(LoadSceneAsync(nextLevelName));

        IEnumerator LoadSceneAsync(string nextLevel)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextLevelName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        SceneManager.LoadScene(nextLevelName);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void testNextButtons() {
        Debug.Log("Next level button pushed");
    }

    void WinScreen()
    {
        OnLevelFinished?.Invoke();
        winText.SetActive(true);

        //Logic for going to the next stage, returning to the main menu, or replaying the current level
        //TODO: Fix problem where it just instantly reloads regardless of input
        // nextLevelButton.onClick.AddListener(testNextButtons);

        // replayButtonWin.onClick.AddListener(testNextButtons);
        
        // //Menu
        // menuButtonWin.onClick.AddListener(testNextButtons);
    }
    
    void LoseScreen()
    {
        Debug.Log("Lose Screen Activated");
        loseText.SetActive(true);

        // replayButtonLose.onClick.AddListener(ReloadLevel);
        // menuButtonLose.onClick.AddListener(GoToMenu);

    }

    void LevelOnLevelFail() {
        Debug.Log("Recieved level fail signal");
        loseCondition = true;
    }
}