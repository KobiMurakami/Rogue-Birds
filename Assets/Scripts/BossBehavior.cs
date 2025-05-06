using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject missilePrefab;
    public GameObject boss;
    public GameObject missileSpawn;
    //public UnityEvent hasFired;
    
    public bool hasFired;
    
    /* TODO: The idea I have for the boss fight is this:
     -Boss cannot be attacked directly
     -A second or two after the bird is launched, the boss will fire a homing missile that follows it (slow so that it isn't too hard)
     -Player must hit the buttons around the stage to expose the boss' weak point*/

    void Start()
    {
        
    }
    void Update()
    {
        float birdHeight = GameObject.FindGameObjectWithTag("Bird").transform.position.y;

        if (birdHeight > 5 && !hasFired)
        {
            FireMissile();
            hasFired = true; // Prevent firing again
        }

        //Reset if bird drops below threshold again
        if (birdHeight <= 5)
        {
            hasFired = false;
        }

        if (GameObject.FindGameObjectWithTag("Bird") == null)
        {
            hasFired = false;
        }
    }

    private void FireMissile()
    {
        Instantiate(missilePrefab, missileSpawn.transform.position, missileSpawn.transform.rotation);
        
    }
}
