using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AbilityGenerator : MonoBehaviour
{
    private string excludedScene = "MainMenu";

    public Button button1;
    public Button button2;
    public Button button3;
    
    private Bird but1Bird;
    private GameObject but2Perk;
    void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == excludedScene)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        but1Bird = BirdBagManager.Instance.GetRandomBirdType();
        but2Perk = PerkManager.Instance.GetRandomPerk();

        button1.GetComponent<Image>().sprite = but1Bird.cardSprite;
        button2.GetComponent<Image>().sprite = but2Perk.GetComponent<SpriteRenderer>().sprite;
    }

    //Add Bird
    public void PressButtonOne()
    {
        BirdBagManager.Instance.AddBird(but1Bird);
        gameObject.SetActive(false);
    }
    
    //Add perk
    public void PressButtonTwo()
    {
        PerkManager.Instance.ActivatePerk(but2Perk);
        gameObject.SetActive(false);
    }
    
    //Add random -----Could make more random affects, or change probability-------------
    public void PressButtonThree()
    {
        if (Random.Range(0f, 1f) > 0.5f)
        {
            BirdBagManager.Instance.AddRandomBirdToBag();
        }
        else
        {
            PerkManager.Instance.ActivatePerk(PerkManager.Instance.GetNonDuplicateRandomPerk());
        }
        gameObject.SetActive(false);
    }
}