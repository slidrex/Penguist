using UnityEngine;

public class IceBreaker : HoldingInteractableObject
{
    [SerializeField] private GameObject escape;
    [SerializeField] private Item iceBreaker;
    public override string InteractString => "Починить";
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return potentialInteractor.Inventory.ContainsItem(iceBreaker);
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(IsInteractable(interactor) == false) 
        {
            (interactor.entity as IHintHolder).Hint.CreateHint("Вот бы кто-нибудь это починил...");
        }
    }
    protected override void OnInteractSuccess()
    {
        FindObjectOfType<Player>().AddRule(Entity.Rule.DisableMovement);
        FindObjectOfType<Player>().AddRule(Entity.Rule.DisableInteraction);
        GameObject e = Instantiate(escape, FindObjectOfType<Camera>().transform.position, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        e.transform.localPosition = new Vector3(0, 0, 0);
    }
}
