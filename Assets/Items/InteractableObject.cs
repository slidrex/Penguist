using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [HideInInspector] public Collider2D Collider;
    public abstract string InteractString { get; }
    private bool interactEnded;
    [HideInInspector] public Interactor Interactor;
    public bool SendInteractRequest(Interactor interactor)
    {
        Interactor = interactor;
        interactEnded = false;
        bool success = IsInteractable(interactor);
        return success;
    }
    public void InteractEnd()
    {
        if(interactEnded == false)
        {
            OnInteractEnd();
            interactEnded = true;
        }
    }
    public void OnInteractorOutOfRange() {}
    protected virtual void OnInteractEnd() { }
    public virtual void OnInteractorInRange(Interactor interactor) {}
    public virtual bool IsInteractable(Interactor potentialInteractor)
    {
        return true;
    }
    public virtual void OnInteractKeyDown()
    {
        
    }
    protected virtual void Update() {}
    public virtual void OnInteractKeyUp()
    {

    }
}
