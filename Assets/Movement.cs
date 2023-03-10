using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Entity entity;
    [SerializeField] protected Animator Animator;
    protected Vector2 MovementVector;
    protected Rigidbody2D rb;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        
    }
    protected void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if(entity.ContainsRule(Entity.Rule.DisableMovement)) MovementVector = Vector2.zero;
        rb.velocity = MovementVector * movementSpeed;
    }
}
