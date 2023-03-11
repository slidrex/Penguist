using UnityEngine;

public class Player : Entity, ICanvasHolder, IHintHolder
{
    [field:SerializeField] public Canvas FollowCanvas { get; set; }
    [field:SerializeField] public PlayerClueHint Hint { get; set; }
}
