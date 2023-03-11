using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 MoveDirection;
    public float BulletSpeed;
    [SerializeField] private float lifeTime;
    private float timeSinceAwake;
    [SerializeField] private GameObject breakParticles;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = MoveDirection * BulletSpeed;
    }
    private void Update()
    {
        if(timeSinceAwake < lifeTime) timeSinceAwake += Time.deltaTime;
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if(breakParticles != null)
            Destroy(Instantiate(breakParticles, transform.position, Quaternion.identity), 5.0f);
    }
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Entity>(out Entity entt))
        {
            entt.Damage();
            Destroy(gameObject);
        }
    }
}
