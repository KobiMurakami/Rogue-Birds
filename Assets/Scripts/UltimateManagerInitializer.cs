using UnityEngine;

public class UltimateManagerInitializer : MonoBehaviour
{

    public GameObject UltimateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(UltimateManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
