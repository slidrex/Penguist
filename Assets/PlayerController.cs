using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    private Vector2 movementDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void SetFacing(Vector2 facing)
    {
        Vector3 rotation = transform.eulerAngles;
        if(facing == Vector2.right) rotation.y = 0.0f;
        else if(facing == Vector2.left) rotation.y = 180.0f;
        transform.eulerAngles = rotation;
    }
    private void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(movementDirection != Vector2.zero) SetFacing(movementDirection.x * Vector2.right);
    }
    private void FixedUpdate()
    {
        rb.velocity = movementDirection * movementSpeed;
    }

}
