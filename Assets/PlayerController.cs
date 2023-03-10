using UnityEngine;

public class PlayerController : Movement
{
    private void SetFacing(Vector2 facing)
    {
        Vector3 rotation = transform.eulerAngles;
        if(facing == Vector2.right) rotation.y = 0.0f;
        else if(facing == Vector2.left) rotation.y = 180.0f;
        transform.eulerAngles = rotation;
    }
    protected override void Update()
    {
        base.Update();
        MovementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(MovementVector != Vector2.zero) SetFacing(MovementVector.x * Vector2.right);
    }
}
