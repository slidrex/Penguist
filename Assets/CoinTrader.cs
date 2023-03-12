using UnityEngine;

public class CoinTrader : InteractableObject
{
    [SerializeField] private Item coin;
    [SerializeField] private Item potion;
    public override string InteractString => "Торговать";
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return potentialInteractor.Inventory.ContainsItem(potion);
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(IsInteractable(interactor) == false)
            (interactor.entity as IHintHolder).Hint.CreateHint("Приходи когда будет антифриз! Просто так денег не дам!");
    }
    public override void OnInteractKeyDown()
    {
        Interactor.Inventory.AddItem(coin);
        Interactor.Inventory.Remove(potion);
        (Interactor.entity as IHintHolder).Hint.CreateHint("Спасибо за покупку!");
        Interactor.InterruptInteraction();
    }
}
