using UnityEngine;

public class PlayerController : Movement
{
    protected override void Update()
    {
        base.Update();
        MovementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(MovementVector != Vector2.zero) SetFacing(MovementVector.x * Vector2.right);
        if(Disabled) MovementVector = Vector2.zero;
        Animator.SetInteger("MoveX", (int)MovementVector.x);
        Animator.SetInteger("MoveY", (int)MovementVector.y);
    }
}
