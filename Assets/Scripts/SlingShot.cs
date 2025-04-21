using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
public class SlingShot : MonoBehaviour
{
    //This script should be the same as the one used for slingshot movement, attached to the slingshot gameobject
    [Header("Line Renderers")]
    [SerializeField] public LineRenderer leftLineRenderer;
    [SerializeField] public LineRenderer rightLineRenderer;

    [Header("Transform References")]
    private Vector2 slingShotLinesPosition;
    public Transform leftStartPosition;
    public Transform rightStartPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;

    [Header("Slingshot settings")]
    [SerializeField] private float maxDistance = 3.5f;

    [SerializeField] private float shotForce = 5f;
    [SerializeField] private float respawnTime = 3f;
    private Vector2 direction;
    private Vector2 directionNormalized;
    private bool birdOnSlingshot;
    private bool clickedWithinArea;

    [SerializeField] private SlingShotArea slingShotArea;
    [SerializeField] private float birdPosOffset = 2f;
    
    private Bird spawnedBird;
    public Bird activeBird;

    

    private void Start()
    {
        SpawnBird();
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.IsWithinSlingshotArea())
        {
            clickedWithinArea = true;
        }
        if(Mouse.current.leftButton.isPressed && clickedWithinArea && birdOnSlingshot)
        {
            Debug.Log("left mouse pressed");
            DrawSlingshot();
            PositionAndRotateBird();
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame && birdOnSlingshot)
        {
            clickedWithinArea = false;
            spawnedBird.LaunchBird(direction, shotForce);
            birdOnSlingshot = false;
            StartCoroutine(spawnBirdAfterTime());
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
        direction = (Vector2)centerPosition.position - slingShotLinesPosition;
        directionNormalized = direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }

    private void SpawnBird()
    {
        SetLines(idlePosition.position);
        Vector2 dir = (centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)idlePosition.position + dir * birdPosOffset;
        
        activeBird = BirdBagManager.Instance.GetBirdForShooting();
        spawnedBird = Instantiate(activeBird, idlePosition.position, Quaternion.identity);
        spawnedBird.transform.right = dir;
        birdOnSlingshot = true;
    }

    private void PositionAndRotateBird()
    {
        spawnedBird.transform.position = slingShotLinesPosition + directionNormalized * birdPosOffset;
        spawnedBird.transform.right = directionNormalized;
    }

    private IEnumerator spawnBirdAfterTime()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnBird();
    }
}
