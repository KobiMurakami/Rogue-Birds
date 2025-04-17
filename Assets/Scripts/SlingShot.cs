using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
public class SlingShot : MonoBehaviour
{
    //This script should be the same as the one used for slingshot movement, attached to the slingshot gameobject
    [SerializeField] public LineRenderer leftLineRenderer;
    [SerializeField] public LineRenderer rightLineRenderer;
    public Transform slingshotStartPosition;
    public Bird activeBird;
    public Transform leftStartPosition;
    public Transform rightStartPosition;

    void Start()
    {
        //Not permanent code, shouldn't be in start prolly
        activeBird = BirdBagManager.Instance.GetBirdForShooting();
    }
    private void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            Debug.Log("left mouse pressed");
            DrawSlingshot();
        }
    }

    //Rerolls the bird, should be called when reroll button is clicked
    private void RerollBird()
    {
        BirdBagManager.Instance.replaceBird(activeBird);
        activeBird = BirdBagManager.Instance.GetBirdForShooting();
        //Animatations
    }
    private void DrawSlingshot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        SetLines(touchPosition);
    }

    private void SetLines(Vector2 position)
    {
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
}
