using UnityEngine;

public class WeatherController : HoldingInteractableObject
{
    private bool isDestroyed = false;
    public override string InteractString => "Уничтожить";
    private string error = "Ошибка";
    public bool IsDestroyed() => isDestroyed;
    protected override void OnInteractSuccess()
    {
        isDestroyed = true;
    }
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(isDestroyed == true)
            interactor.ClueHint.CreateHint(error);
    }
}
