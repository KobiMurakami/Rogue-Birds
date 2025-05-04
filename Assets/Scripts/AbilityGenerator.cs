using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AbilityGenerator : MonoBehaviour
{
    private string excludedScene = "MainMenu";

    public Button button1;
    public Button button2;
    public Button button3;

    public TextMeshProUGUI button1Title;
    public TextMeshProUGUI button2Title;
    public TextMeshProUGUI button3Title;
    
    public TextMeshProUGUI button1Description;
    public TextMeshProUGUI button2Description;
    public TextMeshProUGUI button3Description;
    
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
        //Get abillities
        but1Bird = BirdBagManager.Instance.GetRandomBirdType();
        but2Perk = PerkManager.Instance.GetRandomPerk();

        //Button 1 visuals
        button1.GetComponent<Image>().sprite = but1Bird.cardSprite;
        button1Title.text = but1Bird.birdName;
        button1Description.text = but1Bird.birdDescription;
        
        //Button 2 visuals
        button2.GetComponent<Image>().sprite = but2Perk.GetComponent<SpriteRenderer>().sprite;
        button2Title.text = but2Perk.GetComponent<Perk>().perkName;
        button2Description.text = but2Perk.GetComponent<Perk>().perkDescription;
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