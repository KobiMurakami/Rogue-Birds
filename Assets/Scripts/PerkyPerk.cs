using System;
using UnityEngine;

public class PerkyPerk : MonoBehaviour
{
    //For every perk that is activated, activate another one
    //Hopefully doesn't endless loop itself
    
    private GameObject previousPerk;
    
    void Start()
    {
        PerkManager.OnPerkActivated+= PerkManagerOnOnPerkActivated;
        previousPerk = this.gameObject;
    }

    private void PerkManagerOnOnPerkActivated(GameObject perk)
    {
        if (perk != previousPerk)
        {
            GameObject newPerk = PerkManager.Instance.GetNonDuplicateRandomPerk();
            previousPerk = newPerk;
            PerkManager.Instance.ActivatePerk(newPerk);
        }
    }

    private void OnDestroy()
    {
        PerkManager.OnPerkActivated -= PerkManagerOnOnPerkActivated;
    }
}
