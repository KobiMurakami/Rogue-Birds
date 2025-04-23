using System.Collections.Generic;
using UnityEngine;

public class UltimateManager : MonoBehaviour
{
    //This script is used to instantiate all the other managers, this will keep the order consistent.
    
    public List<GameObject> managers;

    private void Awake()
    {
        foreach (GameObject manager in managers)
        {
            Instantiate(manager);
        }
    }
}
