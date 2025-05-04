using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
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

    [Header("Configuration")]
    public List<Loadout> allLoadouts;
    public List<GameObject> allBirdPrefabs;

    [Header("Runtime State")]
    public List<GameObject> selectedBirds = new List<GameObject>();
    public List<GameObject> selectedPerks = new List<GameObject>();
    public bool usingPresetLoadout = false;

    // Scene references
    private BagInitializer _currentBagInitializer;
    private PerkManager _currentPerkManager;
    private AbilityGenerator _currentAbilityGenerator;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitializeSceneRoutine());
    }
    IEnumerator InitializeSceneRoutine()
    {
        yield return null;
        FindSceneReferences();
        InitializeLevel();
    }

    void FindSceneReferences()
    {
        _currentBagInitializer = GameObject.FindWithTag("BagInitializer").GetComponent<BagInitializer>();
        _currentPerkManager = GameObject.FindWithTag("PerkManager").GetComponent<PerkManager>();
        _currentAbilityGenerator = GameObject.FindWithTag("AbilityGenerator").GetComponent<AbilityGenerator>();
    }

    public void SelectLoadout(Loadout loadout)
    {
        selectedBirds = new List<GameObject>(loadout.birds);
        selectedPerks = new List<GameObject>(loadout.perks);
        usingPresetLoadout = true;
    }

    public void CreateRandomLoadout()
    {
        selectedBirds.Clear();
        int numBirds = Random.Range(3, 5);
        
        for (int i = 0; i < numBirds; i++)
        {
            if (allBirdPrefabs.Count == 0) break;
            int randomIndex = Random.Range(0, allBirdPrefabs.Count);
            selectedBirds.Add(allBirdPrefabs[randomIndex]);
        }
        Debug.Log("Create Random loadout successful");
        usingPresetLoadout = false;
    }

    void InitializeLevel()
    {
        if (_currentBagInitializer != null)
        {
            Debug.Log("Bag Initializer found and activated");
            _currentBagInitializer.SetBirdPrefabs(selectedBirds);
            _currentBagInitializer.PopulateBirdBag();
        }
        else 
        {
            Debug.Log("Unable to find bag initializer");
        }

        if (_currentAbilityGenerator != null)
        {
            _currentAbilityGenerator.gameObject.SetActive(!usingPresetLoadout);
        } 
        else 
        {
            Debug.Log("Unable to find ability generator");
        }

        if (usingPresetLoadout && _currentPerkManager != null)
        {
            foreach(GameObject perk in selectedPerks)
            {
                _currentPerkManager.ActivatePerk(perk);
            }
        }
    }

    public List<Loadout> GetUnlockedLoadouts()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        return allLoadouts.FindAll(loadout => highScore >= loadout.requiredHighScore);
    }
}