using System.Collections;
using UnityEngine;

class BlowfishBird : Bird
{
    [Header("Blowfish Variables")]
    public float shrinkDelay = 8f;
    
    public GameObject needlePrefab; 
    public int numberOfNeedles = 12;
    public float shootForce = 5f;
    
    //Weird ass Necessary Inheritance stuff
    [SerializeField] private float _speedModifier = 1.0f;
    public override float speedModifier
    {
        get { return _speedModifier; }
        set { _speedModifier = value; }
    }

    
    //Expand the bird and shoot out needles
    public override void ActivateAbility()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Expand");
        StartCoroutine(SpawnNeedles());
        StartCoroutine(ShrinkBlowfish());
    }

    private IEnumerator ShrinkBlowfish()
    {
        yield return new WaitForSeconds(shrinkDelay);
        
        gameObject.GetComponent<Animator>().SetTrigger("Contract");
    }

    private IEnumerator SpawnNeedles()
    {
        yield return new WaitForSeconds(.66f); //Hard coded based on animation
        
        float angleStep = 360f / numberOfNeedles;
        float angle = 0f;

        for (int i = 0; i < numberOfNeedles; i++)
        {
            // Calculate direction
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            // Calculate angle of rotation 
            float needleAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0, 0, needleAngle);
            
            // Create needle
            Vector2 spawnPos = (Vector2)transform.position + direction * 1.5f; //Hard coded radius
            GameObject needle = Instantiate(needlePrefab, spawnPos, rotation);
            Rigidbody2D rb = needle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //Calculation and force taking momentum into account
                Rigidbody2D blowfishRb = GetComponent<Rigidbody2D>();
                Vector2 inheritedVelocity = blowfishRb != null ? blowfishRb.linearVelocity : Vector2.zero;
                rb.linearVelocity  = direction * shootForce + inheritedVelocity;
            }

            angle += angleStep;
        }
    }
}
