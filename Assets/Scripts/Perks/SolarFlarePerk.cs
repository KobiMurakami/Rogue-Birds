using System;
using UnityEngine;

public class SolarFlarePerk : Perk
{
    void Start()
    {
        LightningBird.OnLightningSpawned += LightningBirdOnOnLightningSpawned;
    }

    private void LightningBirdOnOnLightningSpawned(GameObject lightning)
    {
        lightning.GetComponent<Lightning>().SetSolarFlareActive();
    }
    

    private void OnDestroy()
    {
        LightningBird.OnLightningSpawned -= LightningBirdOnOnLightningSpawned;
    }
}
