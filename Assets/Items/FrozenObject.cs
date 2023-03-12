using UnityEngine;

public class FrozenObject : InteractableObject
{
    [SerializeField] private Color32 partialFreezeColor = Color.white;
    [SerializeField] private Color32 fullFreezeColor = Color.white;
    [SerializeField] private SpriteRenderer freezedObject;
    private SpriteRenderer iceRenderer;
    public override string InteractString => "Разморозить";
    [SerializeField] private GameObject unfreezeParticles;
    [SerializeField] private Item antiFreeze;
    protected virtual bool AutoRemove  { get; } = true;
    private UnfrozenObject insideObject;
    public void RenderFrozenObject(Sprite frozenObjectSprite, UnfrozenObject insideObject, bool partialPacked)
    {
        iceRenderer = GetComponent<SpriteRenderer>();
        freezedObject.gameObject.SetActive(partialPacked);
        iceRenderer.color = partialPacked ? partialFreezeColor : fullFreezeColor;
        freezedObject.sprite = frozenObjectSprite;
        this.insideObject = insideObject;
    }
    public override bool IsInteractable(Interactor interactor)
    {
        return interactor.Inventory.ContainsItem(antiFreeze);
    }
    public override void OnInteractKeyDown()
    {
        Interactor.Inventory.Remove(antiFreeze);
        OnUnfreeze();
        if(unfreezeParticles != null) Destroy(Instantiate(unfreezeParticles.gameObject, transform.position, Quaternion.identity), 5.0f);
        if(AutoRemove) RemoveFrozenObject();
    }
    public override void OnUnfreeze()
    {
        base.OnUnfreeze();
        var unfrozen = Instantiate(insideObject, transform.position, Quaternion.identity);
        unfrozen.OnUnfreeze();
    }
    protected void RemoveFrozenObject(float time = 0.0f) => Destroy(gameObject, time);
}
