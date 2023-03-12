using UnityEngine;

public class IceBreaker : HoldingInteractableObject
{
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
        int win;
    }
}
