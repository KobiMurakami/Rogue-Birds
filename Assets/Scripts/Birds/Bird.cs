using UnityEngine;

public abstract class Bird : MonoBehaviour
{
    public string birdName;
    public float mass;
    public float speed;
    public float isInMotion;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private bool hasBeenLaunched;
    private bool shouldFaceVelocityDirection;
    private bool powerUsed;

    protected virtual void Awake()
    {
        powerUsed = false;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    protected virtual void Start()
    {
        rb.isKinematic = true;
        circleCollider.enabled = false;
    }

    private void Update()
    {
        //Powerup Activation, currently buggy for some reason
        if (hasBeenLaunched &&  Input.GetKeyDown(KeyCode.Space) && !powerUsed)
        {
            Debug.Log("Powerup");
            ActivateAbility();
            
            //This line may want to be change if birds have multi press powerups
            powerUsed = true;
        }
    }
    private void FixedUpdate()
    {
        if (hasBeenLaunched && shouldFaceVelocityDirection)
        {
            transform.right = rb.linearVelocity;
        }
    }

    public void LaunchBird(Vector2 direction, float force)
    {
        rb.isKinematic = false;
        circleCollider.enabled = true;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        hasBeenLaunched = true;
        shouldFaceVelocityDirection = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFaceVelocityDirection = false;
    }

    public abstract void ActivateAbility();
}