using UnityEngine;

public class BossButton : MonoBehaviour
{
    bool isPressed = false;
    public GameObject weakPoint;
    public GameObject button1;
    public GameObject button2;
    bool isPressed1 = false;
    bool isPressed2 = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weakPoint.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        var buttons = GameObject.FindWithTag("Boss Buttons"); //find gameobjects with the tag "Boss Buttons"

        if (buttons == null)
        {
            weakPoint.SetActive(true);
            Debug.Log("Weak Point SHOULD be revealed!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bird"))
        {
            if (gameObject == button1)
            {
                isPressed1 = true;
                Destroy(button1);
                Debug.Log("Button1 Pressed");
            }

            if (gameObject == button2)
            {
                isPressed2 = true;
                Destroy(button2);
                Debug.Log("Button2 Pressed");
            }
        }
    }
}
