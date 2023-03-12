using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text questText;
    [SerializeField] private Image itemImage;
    private System.Action OnQuestSuccess;
    [SerializeField] private GameObject completeButton;
    private bool questCompleted;
    public void CreateUI(Sprite image, System.Action onQuestSuccess)
    {
        gameObject.SetActive(true);
        questText.text = "";
        completeButton.SetActive(false);
        questCompleted = true;
        itemImage.sprite = image;
        OnQuestSuccess += onQuestSuccess;
    }
    public void AttachImage(Sprite image)
    {
        itemImage.sprite = image;
    }
    public void AddField(string text, int maxValue, int currentValue, bool complete)
    {
        if(complete == false) questCompleted = false;
        Color completeStatus = complete ? Color.green : Color.red;
        if(string.IsNullOrEmpty(questText.text) == false) questText.text += '\n';
        questText.text += $"<color=#{ColorUtility.ToHtmlStringRGBA(completeStatus)}>" + text + $": ({currentValue}/{maxValue})</color>";
    }
    public void EndGenerateUI()
    {
        if(questCompleted) completeButton.SetActive(true);
    }
    public void DestroyUI()
    {
        gameObject.SetActive(false);
    }
    public void CompleteQuest()
    {
        OnQuestSuccess.Invoke();
    }
}
