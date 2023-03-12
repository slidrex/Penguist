using UnityEngine;
using System.Collections.Generic;

public class EntityStatistics : MonoBehaviour
{
    private Dictionary<QuestNPC.QuestHook, int> hooks = new Dictionary<QuestNPC.QuestHook, int>();
    private void Start()
    {
        hooks = new Dictionary<QuestNPC.QuestHook, int>();
    }
    public void ResetStat(QuestNPC.QuestHook hook)
    {
        if(hooks.ContainsKey(hook))
            hooks[hook] = 0;
    }
    public void AddStat(QuestNPC.QuestHook hook, int count)
    {
        if(hooks.ContainsKey(hook) == false) hooks.Add(hook, count);
        else hooks[hook] += count;
        print(count);
    }
    public int GetHookCount(QuestNPC.QuestHook hook)
    {
        if(hooks.ContainsKey(hook) == false) return 0;
        else return hooks[hook];
    }
}
