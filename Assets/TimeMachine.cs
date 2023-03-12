using UnityEngine;
using UnityEngine.SceneManagement;
public class TimeMachine : HoldingInteractableObject
{
    [SerializeField] private Item pulsator;
    [SerializeField] private Item button;
    [SerializeField] private Item battery;
    public override string InteractString => "Собрать машину";
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return potentialInteractor.Inventory.ContainsItem(pulsator) && potentialInteractor.Inventory.ContainsItem(button) && potentialInteractor.Inventory.ContainsItem(battery);
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(IsInteractable(interactor) == false)
            (interactor.entity as IHintHolder).Hint.CreateHint("Недостаточно ресурсов! Запуск невозможен!");
    }
    protected override void OnInteractSuccess()
    {
        Interactor.Inventory.Remove(pulsator);
        Interactor.Inventory.Remove(button);
        Interactor.Inventory.Remove(battery);
        SceneManager.LoadScene(4);
    }
}
