using System.Collections.Generic;
using UnityEngine;

public class UltimateManager : MonoBehaviour
{
    //This script is used to instantiate all the other managers, this will keep the order consistent preventing Null references.
    
    public static UltimateManager Instance { get; set; }
    
    [Header("Managers")]
    public GameObject birdBagManager;
    public GameObject bagInitializer;
    public GameObject perkManager;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
        //Instantiation Order
        Instantiate(birdBagManager);
        Instantiate(bagInitializer);
        Instantiate(perkManager);
    }
    
    
    //TODO potentially have this turn on the slingshots when entering each scene
}