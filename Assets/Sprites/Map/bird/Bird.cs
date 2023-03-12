using UnityEngine;

public class Bird : UnfrozenObject
{
    private Transform target;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 distance = target.position - transform.position;
        rb.velocity = distance.normalized * speed;
        if(distance.x < 0)
            transform.eulerAngles = new Vector2(0, 0);
        else
            transform.eulerAngles = new Vector2(0, 180);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerController>() != null)
        {
            print("Collision");
        }
    }
        
}
