using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.InputSystem.LowLevel;
public class SlingShot : MonoBehaviour
{
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
    
    [Header("Rogue-like settings")]
    public Bird activeBird;

    
    [SerializeField] private int maxRerolls;
    [SerializeField] private int maxShots;

    public int rerollsLeft;
    public int shotsLeft;
    
    
    //Events
    public delegate void ShotFired();
    public static event ShotFired OnShotFired;

   
    

    private void Start()
    {
        //PROBLEM if slingshot is created before bag manager, use Ultimate Manager to make slingshot
        SpawnBird();
        rerollsLeft = maxRerolls;
        shotsLeft = maxShots;
    }

    private void Update()
    {
        if (shotsLeft > 0) //Uncertain this line should cut off everything, also shouldn't call game over the frame after its shot
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.IsWithinSlingshotArea())
            {
                clickedWithinArea = true;
            }

            if (Mouse.current.leftButton.isPressed && clickedWithinArea && birdOnSlingshot)
            {
                Debug.Log("left mouse pressed");
                DrawSlingshot();
                PositionAndRotateBird();
            }
            
            //Bird launch
            if (Mouse.current.leftButton.wasReleasedThisFrame && birdOnSlingshot)
            {
                clickedWithinArea = false;
                spawnedBird.LaunchBird(direction, shotForce);
                birdOnSlingshot = false;
                shotsLeft--;
                OnShotFired?.Invoke();
                if (shotsLeft > 0)
                {
                    StartCoroutine(spawnBirdAfterTime());
                }
            }

            //Reroll activation, NEEDS TO BE CHANGED FROM 'R' TO RAY-CASTED CLICK
            if (Input.GetKeyDown(KeyCode.R) && rerollsLeft > 0)
            {
                RerollBird();
                rerollsLeft--;
            }
        }
        else
        {
            StartCoroutine(waitForGameOver());
            
            //This line is ass but necessary to prevent Null Reference on start up
            GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<LevelController>().loseCondition = true;
            //Should give time for objects falling, otherwise gameover will call right after last shot
        }
    }

    //Rerolls the bird, should be called when reroll button is clicked
    private void RerollBird()
    {
        BirdBagManager.Instance.ReplaceBird(activeBird);
        Destroy(spawnedBird.gameObject);
        SpawnBird();
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

    private IEnumerator waitForGameOver(){
        yield return new WaitForSeconds(5);
    }
}