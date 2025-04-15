using UnityEngine;

public class BagInitializer : MonoBehaviour
{
    //Non-permanent startup script to initialize the bord bag with entries
    void Awake()
    {
        //This should probably be overwritten in the future
        GameObject lightningBird1 = new GameObject("LightningBird1");
        GameObject lightningBird2 = new GameObject("LightningBird2");
        GameObject lightningBird3 = new GameObject("LightningBird3");
        lightningBird1.AddComponent<LightningBird>();
        lightningBird2.AddComponent<LightningBird>();
        lightningBird3.AddComponent<LightningBird>();
        
        BirdBagManager.Instance.AddBird(lightningBird1.GetComponent<LightningBird>());
        BirdBagManager.Instance.AddBird(lightningBird2.GetComponent<LightningBird>());
        BirdBagManager.Instance.AddBird(lightningBird3.GetComponent<LightningBird>());
    }
}
