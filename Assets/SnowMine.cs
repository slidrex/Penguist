using UnityEngine;

public class SnowMine : HoldingInteractableObject
{
    [SerializeField] private Item snow;
    [SerializeField] private int MaxInteractCount;
    private int currentInteractCount;
    private const string holdingSnowError = "У вас уже есть снег!";
    public override void OnUnfreeze()
    {
        base.OnUnfreeze();
        MaxInteractTime = Random.Range(1, 2.0f);
        MaxInteractCount = Random.Range(3, 5);
    }
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return potentialInteractor.Inventory.ContainsItem(snow) == false;
    }
    protected override void OnInteractSuccess()
    {
        Interactor.Inventory.AddItem(snow);
        Interactor.ClueHint.CreateHint(holdingSnowError);

        if(MaxInteractCount > 0)
        {
            if(currentInteractCount < MaxInteractCount)
                currentInteractCount++;
            else Destroy(gameObject);
        }
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(IsInteractable(interactor) == false)
            interactor.ClueHint.CreateHint(holdingSnowError);
    }
}
