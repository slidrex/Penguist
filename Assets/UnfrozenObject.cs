using UnityEngine;

public class UnfrozenObject : MonoBehaviour
{
    public enum IceSize
    {
        Big,
        Small
    }
    public IceSize Size;
    public SpriteRenderer Renderer;
    public virtual void OnUnfreeze()
    {
        
    }
}
