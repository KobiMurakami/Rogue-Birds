using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    
    public TextMeshProUGUI shotText;
    public TextMeshProUGUI rerollText;
    public SlingShot slingShot;

    public Image bagImage;
    public Image perkImage;
    private bool bagButtonPressed = false;
    private bool perkButtonPressed = false;
    
    //Bird amounts
    public TextMeshProUGUI daggerStandbyText;
    public TextMeshProUGUI daggerFieldText;
    public TextMeshProUGUI plainStandbyText;
    public TextMeshProUGUI plainFieldText;
    public TextMeshProUGUI thundrakeStandbyText;
    public TextMeshProUGUI thundrakeFieldText;
    public TextMeshProUGUI ricocrowStandbyText;
    public TextMeshProUGUI ricocrowFieldText;
    public TextMeshProUGUI blowjayStandbyText;
    public TextMeshProUGUI blowjayFieldText;
    public TextMeshProUGUI ashwingStandbyText;
    public TextMeshProUGUI ashwingFieldText;
    
    //Perk images
    public Image bouncyBonusImage;
    public Image heavyWeightImage;
    public Image perkyPerkImage;
    public Image popularPaloozaImage;
    public Image solarFlareImage;
    public Image speedyImage;
    public Image shotsImage;
    public Image birdRandoImage;
    public Image doublePrickImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shotText.text = "Shots Left: " + BirdBagManager.Instance.maxShots;
        rerollText.text = "Rerolls Left: " + BirdBagManager.Instance.maxRerolls;
        
        SlingShot.OnShotFired += SlingShotOnOnShotFired;
        RerollButton.OnRerollPressed += RerollButtonOnOnRerollPressed;
    }
    

    //Adjusts all the numbers of the birds in the HUD
    private void AdjustHUDCount()
    {
        List<string> standByBirds = new List<string>();

        foreach (Bird bird in BirdBagManager.Instance.birdBag)
        {
            standByBirds.Add(bird.birdName);
        }

        int daggerStandByCount = 0;
        int plainStandByCount = 0;
        int thundrakeStandByCount = 0;
        int ricocrowStandByCount = 0;
        int blowjayStandByCount = 0;
        int ashwingStandByCount = 0;

        //Getting amount of each
        foreach (string name in standByBirds)
        {
            switch (name)
            {
                case "Daggerbeak":
                    daggerStandByCount++;
                    break;
                case "Captain Plain":
                    plainStandByCount++;
                    break;
                case "Thundrake":
                    thundrakeStandByCount++;
                    break;
                case "Blowjay":
                    blowjayStandByCount++;
                    break;
                case "Ricocrow":
                    ricocrowStandByCount++;
                    break;
                case "Ashwing":
                    ashwingStandByCount++;
                    break;
                default:
                    Debug.Log(name + " is unknown");
                    break;
            }
        }
        //Setting TMP
        daggerStandbyText.text = daggerStandByCount.ToString(); 
        plainStandbyText.text = plainStandByCount.ToString(); 
        thundrakeStandbyText.text = thundrakeStandByCount.ToString(); 
        blowjayStandbyText.text = blowjayStandByCount.ToString(); 
        ricocrowStandbyText.text = ricocrowStandByCount.ToString(); 
        ashwingStandbyText.text = ashwingStandByCount.ToString();
        
        
        //Do it again for stuff not in bag
        List<string> fieldBirds = new List<string>();
        
        foreach (Bird bird in BirdBagManager.Instance.temporarilyNotInBag)
        {
            fieldBirds.Add(bird.birdName);
        }
        
        int daggerFieldCount = 0;
        int plainFieldCount = 0;
        int thundrakeFieldCount = 0;
        int ricocrowFieldCount = 0;
        int blowjayFieldCount = 0;
        int ashwingFieldCount = 0;
        
        foreach (string name in fieldBirds)
        {
            switch (name)
            {
                case "Daggerbeak":
                    daggerFieldCount++;
                    break;
                case "Captain Plain":
                    plainFieldCount++;
                    break;
                case "Thundrake":
                    thundrakeFieldCount++;
                    break;
                case "Blowjay":
                    blowjayFieldCount++;
                    break;
                case "Ricocrow":
                    ricocrowFieldCount++;
                    break;
                case "Ashwing":
                    ashwingFieldCount++;
                    break;
                default:
                    Debug.Log(name + " is unknown");
                    break;
            }
        }
        
        daggerFieldText.text = daggerFieldCount.ToString();
        plainFieldText.text = plainFieldCount.ToString();
        thundrakeFieldText.text = thundrakeFieldCount.ToString();
        ricocrowFieldText.text = ricocrowFieldCount.ToString();
        blowjayFieldText.text = blowjayFieldCount.ToString();
        ashwingFieldText.text = ashwingFieldCount.ToString();
    }

    //Adjusts the perks opacity based on what's active
    private void AdjustPerkOpacity()
    {
        List<GameObject> perks = PerkManager.Instance.GetAllActivePerks();
        
        List<string> perkNames = new List<string>();
        foreach (GameObject perk in perks)
        {
            perkNames.Add(perk.GetComponent<Perk>().perkName);
        }

        //Tests what is active and sends that info so it can have its opacity changed
        IncreaseOrDecrease(bouncyBonusImage, perkNames.Contains("Bouncy Bonus"));
        IncreaseOrDecrease(heavyWeightImage, perkNames.Contains("Heavy Weight"));
        IncreaseOrDecrease(perkyPerkImage, perkNames.Contains("Perky Perk"));
        IncreaseOrDecrease(popularPaloozaImage, perkNames.Contains("Popular Palooza"));
        IncreaseOrDecrease(solarFlareImage, perkNames.Contains("Solar Flare"));
        IncreaseOrDecrease(speedyImage, perkNames.Contains("Speedy"));
        IncreaseOrDecrease(shotsImage, perkNames.Contains("Shots For Everyone"));
        IncreaseOrDecrease(birdRandoImage,perkNames.Contains("Bird Randomizer"));
        IncreaseOrDecrease(doublePrickImage, perkNames.Contains("Double Prick"));
    }

    //Changes the images opacity based on the bool
    private void IncreaseOrDecrease(Image img, bool shouldIncrease)
    {
        Color c = img.color;
        c.a = shouldIncrease ? 1f : 0.25f;
        img.color = c;
    }

    private void RerollButtonOnOnRerollPressed()
    { 
        rerollText.text = "Rerolls Left: " + slingShot.rerollsLeft;
    }
    private void SlingShotOnOnShotFired()
    {
        shotText.text = "Shots Left: " + slingShot.shotsLeft;
        
    }

    //Disables and Re-enables Bag HUD
    public void BagButtonPressed()
    {
        //Turn off perk if currently on
        if (perkButtonPressed)
        {
            perkImage.gameObject.SetActive(false);
            perkButtonPressed = false;
        }
        
        AdjustHUDCount();
        if (bagButtonPressed)
        {
            bagImage.gameObject.SetActive(false);
            bagButtonPressed = false;
        }
        else
        {
            bagImage.gameObject.SetActive(true);
            bagButtonPressed = true;
        }
    }

    public void PerkButtonPressed()
    {
        //Turn off bag if currently on
        if (bagButtonPressed)
        {
            bagImage.gameObject.SetActive(false);
            bagButtonPressed = false;
        }
        AdjustPerkOpacity();
        if (perkButtonPressed)
        {
           perkImage.gameObject.SetActive(false);
            perkButtonPressed = false;
        }
        else
        {
            perkImage.gameObject.SetActive(true);
            perkButtonPressed = true;
        }
    }

    private void OnDestroy()
    {
        SlingShot.OnShotFired -= SlingShotOnOnShotFired;
        RerollButton.OnRerollPressed -= RerollButtonOnOnRerollPressed;
    }
}
