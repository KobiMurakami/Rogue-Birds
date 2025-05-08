using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
// using UnityEngine.InputSystem.Android.LowLevel;
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
    [SerializeField] private float respawnTime = 5f;
    private Vector2 direction;
    private Vector2 directionNormalized;
    private bool birdOnSlingshot;
    private bool clickedWithinArea;

    [SerializeField] private SlingShotArea slingShotArea;
    [SerializeField] private float birdPosOffset = 2f;
    private Bird spawnedBird;
    
    [Header("Rogue-like settings")]
    public Bird activeBird;
    
    [Header("Audio")]
    [SerializeField] private AudioClip shootsound;



    
    public int rerollsLeft;
    public int shotsLeft;
    public float timeSinceBirdShot;

    public delegate void LevelFailed();
    public static event LevelFailed OnLevelFail;
    public Boolean failSignalSent = false;

    public GameObject levelManager;

    private AudioSource audioSource;
    
    
    //Events
    public delegate void ShotFired();
    public static event ShotFired OnShotFired;

   
    

    private void Start()
    {
        //PROBLEM if slingshot is created before bag manager, use Ultimate Manager to make slingshot
        SpawnBird();
        rerollsLeft = BirdBagManager.Instance.maxRerolls;
        shotsLeft = BirdBagManager.Instance.maxShots;
        audioSource = GetComponent<AudioSource>();
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
            if (Mouse.current.leftButton.wasReleasedThisFrame && birdOnSlingshot && clickedWithinArea) //Fixed the click bug
            {
                clickedWithinArea = false;
                spawnedBird.LaunchBird(direction, shotForce);
                if (shootsound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(shootsound);
                }
                birdOnSlingshot = false;
                shotsLeft--;
                OnShotFired?.Invoke();

                if (shotsLeft > 0)
                {
                    StartCoroutine(spawnBirdAfterTime());
                }
            }
        }
        else
        {
            if(!failSignalSent) {
                Debug.Log("Out of Rerolls");
                StartCoroutine(startFailLevel());
                failSignalSent = true;
            }
        }
    }

    //Rerolls the bird, should be called when reroll button is clicked
    public void RerollBird()
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
        if (activeBird != null)
        {
            spawnedBird = Instantiate(activeBird, idlePosition.position, Quaternion.identity);
            spawnedBird.transform.right = dir;
            spawnedBird.GetComponent<Rigidbody2D>().mass *= BirdBagManager.Instance.massMultiplier;
            spawnedBird.speedModifier *= BirdBagManager.Instance.massMultiplier;
            birdOnSlingshot = true;
        }
        else
        {
            //Case where the bag is empty
            StartCoroutine(startFailLevel()); //This is probably not right, just based on time
        }
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
    private IEnumerator startFailLevel()
    {
        yield return new WaitForSeconds(7f);
        Debug.Log("7 Seconds Passed, sending fail signal");
        OnLevelFail?.Invoke();
    }
}