using UnityEngine;

public class InventoryItemName : MonoBehaviour
{
    [SerializeField] private float nameTime;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private UnityEngine.UI.Text Text;
    private float timeSinceEnabling;
    public void EnableName(string name)
    {
        Text.text = name;
        gameObject.SetActive(true);
        timeSinceEnabling = 0.0f;
    }
    private void Update()
    {
        if(timeSinceEnabling < 1.0f) timeSinceEnabling += Time.deltaTime / nameTime;
        else gameObject.SetActive(false);
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, curve.Evaluate(timeSinceEnabling));
    }
}
