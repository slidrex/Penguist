using UnityEngine;

public class PlayerClueHint : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEngine.UI.Text HintText;
    private const float defaultHintTime = 2.0f;
    private float TargetHintTime;
    private float timeSinceHint;
    private bool disabled;
    public void DisableActive() => gameObject.SetActive(false);
    public void CreateHint(string text, float time = defaultHintTime)
    {
        gameObject.SetActive(true);
        HintText.text = text;
        disabled = false;
        TargetHintTime = time;
        timeSinceHint = 0.0f;
        animator.SetTrigger("Begin");
    }
    private void Update()
    {
        if(timeSinceHint < TargetHintTime)
        {
            timeSinceHint += Time.deltaTime;
        }
        else if(disabled == false)
        {
            DisableHint();
        }
    }
    public void DisableHint()
    {
        disabled = true;
        animator.SetTrigger("End");
    }
}
