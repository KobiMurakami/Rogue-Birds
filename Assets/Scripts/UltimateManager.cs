using System.Collections.Generic;
using UnityEngine;

public class UltimateManager : MonoBehaviour
{
    
    [Header("Managers")]
    public GameObject birdBagManager;
    public GameObject bagInitializer;
    public GameObject perkManager;


    void Start()
    {
        //Instantiation Order
        Instantiate(birdBagManager);
        Instantiate(bagInitializer);
        Instantiate(perkManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO potentially have this turn on the slingshots when entering each scene
}