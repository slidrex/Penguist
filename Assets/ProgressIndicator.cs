using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour
{
    [SerializeField] private Image fill; 
    public void AttachCanvas(Canvas canvas)
    {
        transform.SetParent(canvas.transform);
    }
    public void SetValue(float current, float max)
    {
        fill.fillAmount = current/max;
    }
}
