using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Button menuButton;

    // Update is called once per frame
    void Start()
    {
        int score = PlayerPrefs.GetInt("currentscore", 0);
        if(score >= PlayerPrefs.GetInt("highscore", 0)) {
            PlayerPrefs.SetInt("highscore", score);
        }
        PlayerPrefs.SetInt("currentscore", 0);
        int cumulativeScore = PlayerPrefs.GetInt("cumscore", 0);
        PlayerPrefs.SetInt("cumscore", cumulativeScore + score);
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
