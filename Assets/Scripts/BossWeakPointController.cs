using UnityEngine;

public class BossWeakPointController : MonoBehaviour
{
    public GameObject weakPoint;
    private int pressedCount = 0;

    void Start()
    {
        weakPoint.SetActive(false);
    }

    public void NotifyTargetDestroyed()
    {
        pressedCount++;
        Debug.Log("Button Pressed count: " + pressedCount);

        if (pressedCount >= 2)
        {
            weakPoint.SetActive(true);
            Debug.Log("Weak Point Revealed!");
        }
    }
}

