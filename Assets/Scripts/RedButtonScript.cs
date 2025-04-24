using UnityEngine;

public class RedButtonScript : MonoBehaviour
{
    bool isPressed = false;
    public GameObject redButton;
    public GameObject reaction;

    void Awake()
    {
        isPressed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            ButtonPressed();
        }
    }

    //Commented out while developing; this will destroy the door that holds the rocks. Need someone else to make a Bird tag as I don't want to cause conflict issues.
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            isPressed = true;
        }
    }*/

    void ButtonPressed()
    {
        Destroy(redButton);
        Destroy(reaction);
    }
}
