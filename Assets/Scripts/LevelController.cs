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
            PlayerPrefs.SetInt("currentScore", scoreManager.GetComponent<Scoring>().score);
            WinScreen();
        }

        if(loseCondition) {
            Debug.Log("Lose Condition Triggered");

            int highScore = PlayerPrefs.GetInt("highscore", 0);
            int currentScore = scoreManager.GetComponent<Scoring>().score;
            if(currentScore >= highScore) {
                PlayerPrefs.SetInt("highscore", currentScore);
            }
            if(currentLevelNumber >= PlayerPrefs.GetInt("highestlevel", 0)) {
                PlayerPrefs.SetInt("highestlevel", currentLevelNumber);
            }

            LoseScreen();
        }

        // Debug code for testing win and loss screens
        if (scoreManager.numEnemiesInLevel == scoreManager.numEnemiesKilled)
        {
            winText.SetActive(true);
            if(Input.GetKeyDown(KeyCode.W))
            {
                GoNextLevel();
            }
        }
        if(loseCondition)
        {
            loseText.SetActive(true);
            if(Input.GetKeyDown(KeyCode.R))
            {
                ReloadLevel();
            }
        }
        
        
    }

    void GoNextLevel()
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
        nextLevelButton.onClick.AddListener(GoNextLevel);

        replayButtonWin.onClick.AddListener(ReloadLevel);
        
        //Menu
        menuButtonWin.onClick.AddListener(GoToMenu);
    }
    
    void LoseScreen()
    {
        Debug.Log("Lose Screen Activated");
        loseText.SetActive(true);

        replayButtonLose.onClick.AddListener(ReloadLevel);
        menuButtonLose.onClick.AddListener(GoToMenu);

    }

    void LevelOnLevelFail() {
        Debug.Log("Recieved level fail signal");
        loseCondition = true;
    }
}