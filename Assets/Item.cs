using UnityEngine;


public class Item : ScriptableObject
{
    protected Entity User;
    public virtual void OnAttach(Entity entity) { User = entity;}
    public virtual void OnDrop() { User = null; }
    public virtual void OnItemSecondaryUse(Entity entity) 
    {
        
    }
    public bool DestroyOnDrop;
    public GameObject DestroyEffect;
    public string Name;
    public Sprite Sprite;
}
