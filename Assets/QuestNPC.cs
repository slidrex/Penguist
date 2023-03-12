using UnityEngine;

public class QuestNPC : InteractableObject
{
    public enum QuestHook
    {
        KillBear,
        UnfreezeObjects,
        GetRevolver,
        ReceivedCoins,
        LoseScammer
    }
    [System.Serializable]
    public struct QuestQueue
    {
        public Item RewardItem;
        public Quest[] Quests;
    }
    [System.Serializable]
    public struct Quest
    {
        public QuestHook Hook;
        public string QuestDescription;
        public int Count;
    }
    [SerializeField] private QuestQueue[] quests;
    private int currentQuest;
    public override string InteractString => "Торговать";
    private QuestUI questUI;
    private EntityStatistics stat;
    private bool activated = false;
    private bool blockedNPC;
    public override void OnInteractorInRange(Interactor interactor)
    {
        if(blockedNPC) (interactor.entity as IHintHolder).Hint.CreateHint("Мне уже нечего тебе предложить!");
    }
    public override bool IsInteractable(Interactor potentialInteractor)
    {
        return blockedNPC == false;
    }
    public override void OnInteractKeyDown()
    {

        activated = !activated;
        
        if(activated)
        {
            Interactor.entity.AddRule(Entity.Rule.DisableMovement);
            Interactor.entity.AddRule(Entity.Rule.DisableInteraction);
            OpenCurrentUI();
        }
        else
        {
            EndTrading();
            
        }
    }
    private void EndTrading()
    {
        Interactor.entity.RemoveRule(Entity.Rule.DisableMovement);
        Interactor.entity.RemoveRule(Entity.Rule.DisableInteraction);
        Interactor.InterruptInteraction();
        questUI.DestroyUI();
        activated = false;
    }
    private void OpenCurrentUI()
    {
        
        stat = Interactor.GetComponent<EntityStatistics>();
        questUI = (Interactor.entity as IQuestUIHolder).UI;
        questUI.CreateUI(quests[currentQuest].RewardItem.Sprite, OnQuestSuccess);
        Quest[] _quests = quests[currentQuest].Quests;
        foreach(Quest quest in _quests)
        {
            bool completed = stat.GetHookCount(quest.Hook) >= quest.Count;
            questUI.AddField(quest.QuestDescription, quest.Count, stat.GetHookCount(quest.Hook), completed);
        }
        questUI.EndGenerateUI();
    }
    protected virtual void OnQuestSuccess()
    {
        EndTrading();

        if(currentQuest < quests.Length - 1) 
        {
            currentQuest++;
        }
        else blockedNPC = true;
    }
}
