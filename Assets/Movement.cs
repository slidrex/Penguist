using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Entity entity;
    public Animator Animator;
    protected Vector2 MovementVector;
    protected Rigidbody2D rb;
    public bool Disabled;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetFacing(Vector2 facing)
    {
        if(Disabled)
            return;
        Vector3 rotation = transform.eulerAngles;
        if(facing == Vector2.right) rotation.y = 0.0f;
        else if(facing == Vector2.left) rotation.y = 180.0f;
        transform.eulerAngles = rotation;
    }
    protected virtual void Update()
    {
        Disabled = entity.ContainsRule(Entity.Rule.DisableMovement);
    }
    protected void FixedUpdate()
    {
        HandleMovement();
        OnAfterApplyMovement();
    }
    protected virtual void OnAfterApplyMovement()
    {

    }
    private void HandleMovement()
    {
        if(Disabled) MovementVector = Vector2.zero;
        rb.velocity = MovementVector * movementSpeed;
    }
}
