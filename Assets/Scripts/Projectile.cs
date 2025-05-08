using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform Target = null;
    private GameObject[] targets;
    
    [SerializeField]
    private Rigidbody2D HomingProjectileRB;

    private float _distance;
    private float _closestTarget = Mathf.Infinity;
    public float speed = 9000f;
    public float rotationSpeed = 9000f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HomingProjectileRB = GetComponent<Rigidbody2D>();

        if (HomingProjectileRB == null)
        {
            Debug.LogError("The rigidbody is null");
        }
        FindTargets();
    }

    // Update is called once per frame
    void Update()
    {
        FireProjectile();
    }

    private void FindTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Bird");

        foreach (var bird in targets)
        {
            _distance = (bird.transform.position - this.transform.position).sqrMagnitude;

            if (_distance < _closestTarget)
            {
                _closestTarget = _distance;
                Target = bird.transform;
            }
        }
    }

    private void FireProjectile()
    {
        HomingProjectileRB.linearVelocity = transform.up * speed * Time.deltaTime;
        if (Target != null)
        {
            Vector2 direction = (Vector2)Target.position - HomingProjectileRB.position;
            direction.Normalize();
            float rotationValue = Vector3.Cross(direction, transform.up).z;
            HomingProjectileRB.angularVelocity = -rotationValue * rotationSpeed;
            HomingProjectileRB.linearVelocity = transform.up * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    
}
