using UnityEngine;

public abstract class UnfrozenObject : MonoBehaviour
{
    public SpriteRenderer Renderer;
    protected virtual void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    public virtual void OnUnfreeze() {}
    public virtual void OnDamage() {}
}
