using UnityEngine;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }

    [Header("Content Configuration")]
    public List<Bird> allBirds = new List<Bird>();
    public List<GameObject> allPerks = new List<GameObject>();

    [Header("Level Unlocks")]
    public List<LevelUnlock> levelUnlocks = new List<LevelUnlock>();

    [Header("Current Session")]
    public List<Bird> selectedBirds = new List<Bird>();
    public List<GameObject> selectedPerks = new List<GameObject>();

    public GameObject slingshotPrefab;

    private bool _isInitialized;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<Bird> GetAvailableBirds(int currentLevel)
    {
        return levelUnlocks.Find(l => l.level == currentLevel)?.availableBirds ?? new List<Bird>();
    }

    public List<GameObject> GetAvailablePerks(int currentLevel)
    {
        return levelUnlocks.Find(l => l.level == currentLevel)?.availablePerks ?? new List<GameObject>();
    }

    public void ApplySelections()
    {
        // Clear previous selections
        BirdBagManager.Instance.ResetBag();
        foreach(GameObject perk in allPerks)
        {
            PerkManager.Instance.DeactivatePerk(perk);
        }
        

        // Add new birds
        foreach (Bird bird in selectedBirds)
        {
            BirdBagManager.Instance.AddBird(Instantiate(bird));
        }

        // Activate perks
        foreach (GameObject perk in selectedPerks)
        {
            PerkManager.Instance.ActivatePerk(perk.gameObject);
        }
    }

    public void InitializeSlingshot()
    {
        // Call after ApplySelections
        Instantiate(slingshotPrefab);
    }
}

[System.Serializable]
public class LevelUnlock
{
    public int level;
    public List<Bird> availableBirds;
    public List<GameObject> availablePerks;
}