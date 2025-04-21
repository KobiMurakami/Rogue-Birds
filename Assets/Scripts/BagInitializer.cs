using UnityEngine;

public class BagInitializer : MonoBehaviour
{
    //Non-permanent startup script to initialize the bord bag with entries
    
    [SerializeField] private Bird defaultBirdPrefab;

    private void Start()
    {
        //Generating bag
        for (int i = 0; i < 3; i++)
        {
            BirdBagManager.Instance.AddBird(defaultBirdPrefab);
        }
    }
}
