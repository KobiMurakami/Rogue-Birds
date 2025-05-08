using UnityEngine;

public class BossButton : MonoBehaviour
{
    public GameObject weakPoint;
    public GameObject button1;
    public GameObject button2;
    public bool isPressed1 = false;
    public bool isPressed2 = false;
    
    public int pressedCount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weakPoint.SetActive(false);
        pressedCount = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        //var buttons = GameObject.FindWithTag("Boss Buttons"); //find game objects with the tag "Boss Buttons"

        //Method 1
        if (isPressed1 && isPressed2)
        {
            weakPoint.SetActive(true);
            Debug.Log("Weak Point Button SHOULD be revealed through the isPressed method!");
        }
        
        //Method 2
        if (pressedCount >= 2)
        {
            weakPoint.SetActive(true);
            Debug.Log("Weak Point Button SHOULD be revealed through the Notify method!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bird") || collision.gameObject.CompareTag("Lightning") || collision.gameObject.CompareTag("Needle"))
        {
            if (gameObject == button1)
            {
                NotifyTargetDestroyed();
                isPressed1 = true; 
                button1.SetActive(false);
                Debug.Log("Button1 Pressed");
            }

            else if (gameObject == button2)
            {
                NotifyTargetDestroyed();
                isPressed2 = true;
                button2.SetActive(false);
                Debug.Log("Button2 Pressed");
            }
        }
    }

    
    
    private void NotifyTargetDestroyed()
    {
        pressedCount++;
        Debug.Log("Button Pressed count " + pressedCount);
    }
}
