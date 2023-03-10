using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Rule
    {
        DisableInteraction,
        DisableMovement
    }
    private System.Collections.Generic.List<Rule> rules = new System.Collections.Generic.List<Rule>();
    public bool ContainsRule(Rule rule) => rules.Contains(rule);
    public void AddRule(Rule rule) => rules.Add(rule);
    public void RemoveRule(Rule rule) => rules.Remove(rule);
}
