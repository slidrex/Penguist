using UnityEngine;

public class SnowMine : HoldingInteractableObject
{
    [SerializeField] private Item snow;
    [SerializeField] protected int MaxUses;
    private int useCount;
    private const string holdingSnowError = "У вас уже есть снег!";
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return potentialInteractor.Inventory.ContainsItem(snow) == false;
    }
    protected override void OnInteractSuccess()
    {
        Interactor.Inventory.AddItem(snow);
        Interactor.ClueHint.CreateHint(holdingSnowError);
        useCount++;
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(IsInteractable(interactor) == false)
            interactor.ClueHint.CreateHint(holdingSnowError);
    }
}
