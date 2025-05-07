using UnityEngine;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }

    [System.Serializable]
    public class Loadout
    {
        public string loadoutName;
        public int requiredHighScore;
        public List<GameObject> birds;
        public List<GameObject> perks;
    }

    [Header("Loadout Configuration")]
    public List<Loadout> allLoadouts = new List<Loadout>(6); // Set size to 6 in Inspector

    [Header("Current Selection")]
    public List<GameObject> selectedBirds = new List<GameObject>();
    public List<GameObject> selectedPerks = new List<GameObject>();
    public bool usingPresetLoadout = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this); 

    }

    public void SelectLoadout(int loadoutIndex)
    {
        if (loadoutIndex < 0 || loadoutIndex >= allLoadouts.Count) return;

        var loadout = allLoadouts[loadoutIndex];
        selectedBirds = new List<GameObject>(loadout.birds);
        selectedPerks = new List<GameObject>(loadout.perks);
        usingPresetLoadout = true;
    }

    public void CreateRandomLoadout()
    {
        selectedBirds.Clear();
        selectedPerks.Clear();
        
        int numBirds = Random.Range(3, 6);
        for (int i = 0; i < numBirds; i++)
        {
            if (allLoadouts.Count == 0) break;
            int randomIndex = Random.Range(0, allLoadouts[0].birds.Count);
            selectedBirds.Add(allLoadouts[0].birds[randomIndex]);
        }
        usingPresetLoadout = false;
    }

    public void ApplyLoadout()
    {
        if (!usingPresetLoadout && selectedBirds.Count == 0)
        {
            CreateRandomLoadout();
        }

        // Apply to persistent managers
        BirdBagManager.Instance.ClearBag();
        foreach (var birdPrefab in selectedBirds)
        {
            BirdBagManager.Instance.AddBird(birdPrefab.GetComponent<Bird>());
        }

        if (usingPresetLoadout)
        {
            PerkManager.Instance.DeactivateAllPerks();
            foreach (var perkPrefab in selectedPerks)
            {
                PerkManager.Instance.ActivatePerk(perkPrefab);
            }
        }
    }

    public bool IsLoadoutUnlocked(int loadoutIndex)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        return highScore >= (loadoutIndex + 1) * 5000;
    }
}