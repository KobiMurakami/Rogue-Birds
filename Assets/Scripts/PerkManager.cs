using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance { get; set; }
    
    //Events
    public delegate void PerkActivated(GameObject perk);
    public static event PerkActivated OnPerkActivated;
    public delegate void PerkDeactivated(GameObject perk);
    public static event PerkDeactivated OnPerkDeactivated;
    
    //Vars
    private List<GameObject> allPerks = new List<GameObject>();
    private List<GameObject> activePerks = new List<GameObject>();
    
    public int maxPerks;
    public BirdBagManager birdBag;
    
    [Header("Perks")]
    //Change variable names in future
    public GameObject perkname1;
    public GameObject perkname2;
    public GameObject perkname3;
    
    
    
    
    
    
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
        DontDestroyOnLoad(gameObject);
        
        //Adding all perks to the set, probably a better way to do this, -----EACH NEW PERK MUST BE ADDED IN AWAKE-----
        allPerks.Add(perkname1);
        allPerks.Add(perkname2);
        allPerks.Add(perkname3);
    }

    //Instantiates the perk game object and adds it to the active list
    public void ActivatePerk(GameObject perk)
    {
        if (activePerks.Contains(perk))
        {
            Debug.Log("ERROR " + perk.name + " is already active");
            return;
        }
        if (activePerks.Count >= maxPerks)
        {
            Debug.Log("ERROR " + "Max perks reached");
            return;
        }
        
        activePerks.Add(perk);
        Instantiate(perk, perk.transform.position, perk.transform.rotation);
        OnPerkActivated?.Invoke(perk);
    }

    //Destroys perk object and removes it from active list
    public void DeactivatePerk(GameObject perk)
    {
        if (!activePerks.Contains(perk))
        {
            Debug.Log("ERROR " + perk.name + " is not active");
            return;
        }
        activePerks.Remove(perk);
        Destroy(perk);
        OnPerkDeactivated?.Invoke(perk);
    }
    

    //Gets a random perks game object from total perks
    public GameObject GetRandomPerk()
    {
        return allPerks[Random.Range(0, allPerks.Count)];
    }

    //Gets a random perk that is not currently active
    public GameObject GetNonDuplicateRandomPerk()
    {
        //Sanity check
        if (activePerks.Count >= allPerks.Count)
        {
            Debug.Log("All available perks are already active. Cannot select a non-duplicate. Something is very broken");
            return null;
        }
        
        GameObject tempPerk;
        do
        {
            tempPerk = GetRandomPerk();
        } while (IsActivePerk(tempPerk));

        return tempPerk;
    }
    
    public GameObject GetRandomActivePerk()
    {
        return activePerks[Random.Range(0, activePerks.Count)];
    }

    public List<GameObject> GetAllActivePerks()
    {
        return activePerks;
    }
    
    public bool IsActivePerk(GameObject perk)
    {
        return activePerks.Contains(perk);
    }
}

