using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class UltimateManager : MonoBehaviour
{
    public static UltimateManager Instance { get; private set; }

    [Header("Manager Prefabs")]
    public GameObject birdBagManagerPrefab;
    public GameObject perkManagerPrefab;
    public GameObject progressionManagerPrefab;
    public GameObject loadoutManagerPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        // BirdBagManager
        if (!FindObjectOfType<BirdBagManager>())
            Instantiate(birdBagManagerPrefab, transform);
            
        // PerkManager
        if (!FindObjectOfType<PerkManager>())
            Instantiate(perkManagerPrefab, transform);
            
        // ProgressionManager
        if (!FindObjectOfType<ProgressionManager>())
            Instantiate(progressionManagerPrefab, transform);
            
        // LoadoutManager (only when needed)
        if (!FindObjectOfType<LoadoutManager>() && 
            SceneManager.GetActiveScene().name == "Loadout")
        {
            Instantiate(loadoutManagerPrefab, transform);
        }
    }

    public void CreateLoadoutManager()
    {
        if (!FindObjectOfType<LoadoutManager>())
            Instantiate(loadoutManagerPrefab, transform);
    }
}