using TMPro;
using UnityEngine;

public class MenuScoreBoard : MonoBehaviour
{

    public TextMeshProUGUI HighScoreNumber;
    public TextMeshProUGUI HighLevelNumber;
    public TextMeshProUGUI CumulativeScoreNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("highscore", 0);
        int highestLevel = PlayerPrefs.GetInt("highestlevel", 0);
        int cumulativeScore = PlayerPrefs.GetInt("cumscore", 0);

        HighScoreNumber.text = highScore.ToString();
        HighLevelNumber.text = highestLevel.ToString();
        CumulativeScoreNumber.text = cumulativeScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
