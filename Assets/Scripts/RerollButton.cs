using UnityEngine;

public class RerollButton : MonoBehaviour
{
    
    //Events 
    public delegate void RerollPressed();
    public static event RerollPressed OnRerollPressed;

    
    private SlingShot parentScript;

    void Start()
    {
        // Get the parent script once
        parentScript = GetComponentInParent<SlingShot>();
    }
    
    void Update()
    {
        //Reroll activation
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Reroll button clicked!");
                
                if (parentScript.rerollsLeft > 0)
                {
                    parentScript.RerollBird();
                    parentScript.rerollsLeft--;
                    OnRerollPressed?.Invoke();
                }
                //Useless snippet
                else
                {
                    Debug.Log("No rerolls left!");
                }
            }
        }
        
        //Reroll activation, IMPLEMENTED ON SPRITE SCRIPT ALREADY, THIS SHOULD BE REMOVED ONCE THE CLICK BUG IS FIXED, TEMPORARY BACK UP USING 'R' KEY
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reroll button clicked!");
                
            if (parentScript.rerollsLeft > 0)
            {
                parentScript.RerollBird();
                parentScript.rerollsLeft--;
            }
            //Useless snippet
            else
            {
                Debug.Log("No rerolls left!");
            }
        }
        */
    }
}
