using UnityEngine;

public class Player : Entity, ICanvasHolder, IHintHolder, IQuestUIHolder
{
    [SerializeField] private Item coin;
    [SerializeField] private Item revolver;
    [field:SerializeField] public Canvas FollowCanvas { get; set; }
    [field:SerializeField] public PlayerClueHint Hint { get; set; }
    [field:SerializeField] public QuestUI UI { get; set; }
    private Inventory inventory;
    private EntityStatistics statistics;
    public GameObject LoseImage;
    [SerializeField] private ItemDatabase database;
    private void Start()
    {
        statistics = GetComponent<EntityStatistics>();
        inventory = GetComponent<InventoryHolder>().Inventory;
        database.Reset();
        if(inventory != null)
            inventory.OnInventoryChanged += UpdateStats;
    }
    private void UpdateStats()
    {
        int coinCount = 0;
        int revolverCount = 0;
        foreach(Item item in inventory.Items)
        {
            if(item != null && item.Name == coin.Name) coinCount++;
            else if(item != null && item.Name == revolver.Name) revolverCount++;
        }
        statistics.ResetStat(QuestNPC.QuestHook.GetRevolver);
        statistics.ResetStat(QuestNPC.QuestHook.ReceivedCoins);
        statistics.AddStat(QuestNPC.QuestHook.GetRevolver, revolverCount);
        statistics.AddStat(QuestNPC.QuestHook.ReceivedCoins, coinCount);
    }
}
