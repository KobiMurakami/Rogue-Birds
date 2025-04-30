using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBackgroundManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] birdPrefabs; 
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRate = 1.5f;
    [SerializeField] private float minSpawnDelay = 0.5f;
    [SerializeField] private float maxSpawnDelay = 3.0f;
    
    [Header("Movement Settings")]
    [SerializeField] private float minSpeed = 3.0f;
    [SerializeField] private float maxSpeed = 8.0f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float gravity = 9.8f;
    
    [Header("Spawn Locations")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    
    private List<MenuFlyingObject> activeObjects = new List<MenuFlyingObject>();
    private Camera mainCamera;
    private float screenWidth;
    private float nextSpawnTime;
    
    void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2;
        
        // Create default spawn points if not assigned
        if (leftSpawnPoint == null)
        {
            GameObject spawnPoint = new GameObject("LeftSpawnPoint");
            spawnPoint.transform.position = new Vector3(-screenWidth/2 - 1, -mainCamera.orthographicSize / 2, 0);
            leftSpawnPoint = spawnPoint.transform;
            leftSpawnPoint.transform.parent = transform;
        }
        
        if (rightSpawnPoint == null)
        {
            GameObject spawnPoint = new GameObject("RightSpawnPoint");
            spawnPoint.transform.position = new Vector3(screenWidth/2 + 1, -mainCamera.orthographicSize / 2, 0);
            rightSpawnPoint = spawnPoint.transform;
            rightSpawnPoint.transform.parent = transform;
        }
        
        nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        
        // Start with a few objects already on screen
        for (int i = 0; i < 3; i++)
        {
            SpawnRandomObject();
        }
    }
    
    void Update()
    {
        // Check if it's time to spawn a new object
        if (Time.time >= nextSpawnTime && SceneManager.GetActiveScene().name == "MainMenu") //Austins fix to ensure this only happens on Main Menu
        {
            SpawnRandomObject();
            nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        }
        
        // Delete objects that have gone off screen
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            if (activeObjects[i] == null || !activeObjects[i].gameObject.activeInHierarchy)
            {
                activeObjects.RemoveAt(i);
                continue;
            }
            
            // Check if object is off screen
            Vector3 screenPos = mainCamera.WorldToViewportPoint(activeObjects[i].transform.position);
            if (screenPos.x < -0.1f || screenPos.x > 1.1f || screenPos.y < -0.1f || screenPos.y > 1.1f)
            {
                Destroy(activeObjects[i].gameObject);
                activeObjects.RemoveAt(i);
            }
        }
    }
    
    void SpawnRandomObject()
    {
        // Randomly select bird or enemy
        GameObject[] prefabArray = Random.value > 0.5f ? birdPrefabs : enemyPrefabs;
        
        if (prefabArray.Length == 0) return;
        
        GameObject prefab = prefabArray[Random.Range(0, prefabArray.Length)];
        
        // Randomly choose direction
        bool movingRight = Random.value > 0.5f;
        Transform spawnPoint = movingRight ? leftSpawnPoint : rightSpawnPoint;
        
        GameObject newObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        MenuFlyingObject flyingObj = newObj.AddComponent<MenuFlyingObject>();
        
        float angle;
        if (movingRight)
        {
            angle = Random.Range(20f, 70f);
        }
        else
        {
            angle = Random.Range(110f, 160f);
        }
        
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        float speed = Random.Range(minSpeed, maxSpeed);
        
        // Flip sprite if moving left
        SpriteRenderer spriteRenderer = newObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && !movingRight)
        {
            spriteRenderer.flipX = true;
        }
        
        flyingObj.Initialize(direction, speed, gravity, rotationSpeed);
        activeObjects.Add(flyingObj);
    }
}

public class MenuFlyingObject : MonoBehaviour
{
    private Vector2 velocity;
    private float gravity;
    private float rotationSpeed;
    private float spinDirection;
    
    public void Initialize(Vector2 direction, float speed, float gravity, float rotSpeed)
    {
        velocity = direction.normalized * speed;
        this.gravity = gravity;
        this.rotationSpeed = rotSpeed;
        spinDirection = Random.value > 0.5f ? 1f : -1f;
    }
    
    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
        transform.Rotate(0, 0, rotationSpeed * spinDirection * Time.deltaTime);
    }
}