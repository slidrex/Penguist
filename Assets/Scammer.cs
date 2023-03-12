using UnityEngine;

public class Scammer : InteractableObject
{
    [SerializeField] private Enemy bird;
    [SerializeField] private Item coin;
    public override string InteractString => "Взаимодействовать";
    private bool requested;
    private float interactInterval = 1.0f;
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
            (Interactor.entity as IHintHolder).Hint.CreateHint("Да подожди ты!");
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
                Interactor.GetComponent<EntityStatistics>().AddStat(QuestNPC.QuestHook.WinScammer, 1);
            }
            else 
            {
                (Interactor.entity as IHintHolder).Hint.CreateHint("Не повезло! Не повезло!");
                Enemy brd = Instantiate(bird, (Vector2)Interactor.entity.transform.position + 5 * RandVector(), Quaternion.identity);
                Interactor.GetComponent<EntityStatistics>().AddStat(QuestNPC.QuestHook.LoseScammer, 1);
            }
            Interactor.Inventory.AddItem(coin);
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
    private Vector2 RandVector()
    {
        int rand = Random.Range(0, 4);
        switch(rand)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.right;
            case 2:
                return Vector2.down;
            case 3:
                return Vector2.left;
            default:
                return Vector2.zero;
        }
    }
}
