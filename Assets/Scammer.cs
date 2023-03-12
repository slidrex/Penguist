using UnityEngine;

public class Scammer : InteractableObject
{
    [SerializeField] private Enemy bird;
    [SerializeField] private Item coin;
    public override string InteractString => "Взаимодействовать";
    private bool requested;
    private float interactInterval = 1.0f;
    private float additionalChance;
    private float timeSinceInteract;
    protected override void Awake()
    {
        base.Awake();
        additionalChance = 0.0f;
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
            if(Random.Range(0, 1f) <= 0.5f + additionalChance)
            {
                (Interactor.entity as IHintHolder).Hint.CreateHint("Повезло повезло!");
                if(additionalChance > 0) additionalChance -= 0.15f;;
                Interactor.GetComponent<EntityStatistics>().AddStat(QuestNPC.QuestHook.WinScammer, 1);
                Interactor.Inventory.AddItem(coin);
            }
            else 
            {
                (Interactor.entity as IHintHolder).Hint.CreateHint("Не повезло! Не повезло!");
                Enemy brd = Instantiate(bird, (Vector2)Interactor.entity.transform.position + 5 * RandVector(), Quaternion.identity);
                additionalChance += 0.15f;
                Interactor.GetComponent<EntityStatistics>().AddStat(QuestNPC.QuestHook.LoseScammer, 1);
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
