using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

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
    private bool isInitializing;
    private Scene currentScene;
    
    private int lastSceneHash;

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
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene == currentScene) return;
        currentScene = scene;
        int currentHash = GetSceneHash(scene);
        if(currentHash != lastSceneHash)
        {
            lastSceneHash = currentHash;
            StartCoroutine(InitializeSceneRoutine());
        }
    }

    int GetSceneHash(Scene scene)
    {
        return scene.buildIndex.GetHashCode() ^ scene.name.GetHashCode();
    }
    IEnumerator InitializeSceneRoutine()
    {
        if(isInitializing) yield break;
        isInitializing = true;
        yield return null;
        yield return null;
        yield return null;
        try
        {
            Debug.Log($"Initializing Scene: {currentScene.name}");
            FindSceneReferences();
            InitializeLevel();
        }
        finally
        {
            isInitializing = false;
        }
    }

    void FindSceneReferences()
    {
        _currentBagInitializer = null;
        _currentAbilityGenerator = null;
        _currentPerkManager = null;
        GameObject[] rootObjects = currentScene.GetRootGameObjects();
        foreach(GameObject root in rootObjects)
        {
            if(!_currentBagInitializer)
            {
                _currentBagInitializer = root.GetComponentInChildren<BagInitializer>(true);
            }
            if(!_currentPerkManager)
            {
                _currentPerkManager = root.GetComponentInChildren<PerkManager>(true);
            }
            try
            {
                if(!_currentAbilityGenerator)
                {
                    _currentAbilityGenerator = root.GetComponentInChildren<AbilityGenerator>(true);
                }
            } 
            catch 
            {
                Debug.Log("No ability generator in scene");
            }
            
        }
        Debug.Log($"References found - BagInitializer: {_currentBagInitializer != null}," + $"Perk: {_currentPerkManager != null}," + $"Ability: {_currentAbilityGenerator}");
        /*Debug.Log("FindSceneReferences running");
        _currentBagInitializer = GameObject.FindWithTag("BagInitializer").GetComponent<BagInitializer>();
        _currentPerkManager = GameObject.FindWithTag("PerkManager").GetComponent<PerkManager>();
        try
        {
            _currentAbilityGenerator = GameObject.FindWithTag("AbilityGenerator").GetComponent<AbilityGenerator>();
        }
        catch
        {
            Debug.Log("No Ability Generator in scene");
        }
        */
        
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
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}