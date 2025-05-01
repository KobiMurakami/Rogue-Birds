using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;
    
    public TMP_Text highscoreText;
    private int highscore;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        UpdateHighScoreUI();
    }
    public void TrySetNewHighScore(int score)
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    public void UpdateHighScoreUI()
    {
        if (highscoreText != null)
        {
            highscoreText.text = "High Score: " + highscore;
        }
    }
}
