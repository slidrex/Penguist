using UnityEngine;

public class Scammer : InteractableObject
{
    [SerializeField] private Bird bird;
    [SerializeField] private Item coin;
    public override string InteractString => "Взаимодействовать";
    private bool requested;
    private float interactInterval = 16.0f;
    private float timeSinceInteract;
    protected override void Awake()
    {
        base.Awake();
        timeSinceInteract = interactInterval;
    }
    public override void OnInteractKeyDown()
    {
        if(timeSinceInteract < interactInterval)
        {
            (Interactor.entity as IHintHolder).Hint.CreateHint("Я устал, приходи чуть позже!");
            return;
        }
        if(requested == false)
        {
            (Interactor.entity as IHintHolder).Hint.CreateHint("Хочешь сыграть в игру?");
            requested = true;
        }
        else 
        {
            if(Random.Range(0, 1f) <= 0.5f)
            {
                (Interactor.entity as IHintHolder).Hint.CreateHint("Повезло повезло!");
                Interactor.Inventory.AddItem(coin);
            }
            else 
            {
                (Interactor.entity as IHintHolder).Hint.CreateHint("Не повезло! Не повезло!");
                //Interactor.transform -  игрок
                //bir - птица
            }
            timeSinceInteract = 0.0f;
            Interactor.InterruptInteraction();
        }
    }
    protected override void Update()
    {
        base.Update();
        if(timeSinceInteract < interactInterval) timeSinceInteract += Time.deltaTime;
    }
    protected override void OnInteractEnd()
    {
        requested = false;
    }
}
