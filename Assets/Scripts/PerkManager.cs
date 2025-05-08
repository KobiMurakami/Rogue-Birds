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
    public List<GameObject> allPerks = new List<GameObject>();
    public List<GameObject> activePerks = new List<GameObject>();
    
    public int maxPerks;
    
    
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

    public void DeactivateAllPerks()
    {
        foreach(GameObject perk in allPerks)
        {
            if(activePerks.Contains(perk))
            {
                DeactivatePerk(perk);
            }
        }
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

