using UnityEngine;

public class PotionTable : InteractableObject
{
    [SerializeField] private Item potionMaterial;
    [SerializeField] private Item outputItem;
    public override string InteractString => "Сделать зелье";
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return potentialInteractor.Inventory.ContainsItem(potionMaterial);
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(IsInteractable(interactor) == false)
            (interactor.entity as IHintHolder).Hint.CreateHint("Недостаточно снега!");
    }
    public override void OnInteractKeyDown()
    {
        Interactor.Inventory.Remove(potionMaterial);
        Interactor.Inventory.AddItem(outputItem);
    }
}
