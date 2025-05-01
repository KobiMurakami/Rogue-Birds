using UnityEngine;

public class LoadoutScreenInitializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       UltimateManager.Instance.CreateLoadoutManager(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
