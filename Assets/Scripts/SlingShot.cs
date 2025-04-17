using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
public class SlingShot : MonoBehaviour
{
    //This script should be the same as the one used for slingshot movement, attached to the slingshot gameobject
    [SerializeField] public LineRenderer leftLineRenderer;
    [SerializeField] public LineRenderer rightLineRenderer;
    public Bird activeBird;
    public Transform leftStartPosition;
    public Transform rightStartPosition;

    [SerializeField] private float maxDistance = 3.5f;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private SlingShotArea slingShotArea;
    private Vector2 slingShotLinesPosition;
    private bool clickedWithinArea;

    void Start()
    {
        //Not permanent code, shouldn't be in start prolly
        activeBird = BirdBagManager.Instance.GetBirdForShooting();
    }
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.IsWithinSlingshotArea())
        {
            clickedWithinArea = true;
        }
        if(Mouse.current.leftButton.isPressed && clickedWithinArea)
        {
            Debug.Log("left mouse pressed");
            DrawSlingshot();
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clickedWithinArea = false;
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
        slingShotLinesPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);
        SetLines(slingShotLinesPosition);
    }

    private void SetLines(Vector2 position)
    {
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
}
