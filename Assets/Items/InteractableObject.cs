using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract string InteractString { get; }
    private bool interactEnded;
    [HideInInspector] public Interactor Interactor;
    public bool SendInteractRequest(Interactor interactor)
    {
        Interactor = interactor;
        interactEnded = false;
        return OnInteractRequest();
    }
    public void InteractEnd()
    {
        if(interactEnded) return;
        OnInteractEnd();
        interactEnded = true;
        Interactor = null;
    }
    protected virtual void OnInteractEnd() {}
    protected virtual bool OnInteractRequest()
    {
        return true;
    }
    public virtual void OnInteract()
    {

    }
}
