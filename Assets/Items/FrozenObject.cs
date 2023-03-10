using UnityEngine;

public abstract class FrozenObject : InteractableObject
{
    private enum FreezeType
    {
        Full,
        Partial
    }
    [SerializeField] private SpriteRenderer freezedObject;
    public override string InteractString => "Разморозить";
    [SerializeField] private FreezeType freezeType;
    private const string antiFreezeName = "Антифриз";
    protected abstract Sprite RenderedObject { get; }
    protected virtual bool AutoRemove  { get; } = true;
    protected virtual void Start()
    {
        RenderFrozenObject();
    }
    private void RenderFrozenObject()
    {
        freezedObject.gameObject.SetActive(freezeType == FreezeType.Partial);
        freezedObject.sprite = RenderedObject;
    }
    protected override bool OnInteractRequest()
    {
        return Interactor.Inventory.ContainsItem(antiFreezeName);
    }
    public override void OnInteract()
    {
        Interactor.Inventory.Remove(antiFreezeName);
        OnUnfreeze();
        if(AutoRemove) RemoveFrozenObject();
    }
    protected virtual void OnUnfreeze()
    {

    }
    protected void RemoveFrozenObject(float time = 0.0f) => Destroy(gameObject, time);
}
