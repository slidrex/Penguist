using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text questText;
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    private System.Action OnQuestSuccess;
    [SerializeField] private GameObject completeButton;
    private bool questCompleted;
    private bool completed;
    public void CreateUI(Sprite image, string itemName, System.Action onQuestSuccess)
    {
        this.itemName.text = itemName;
        gameObject.SetActive(true);
        questText.text = "";
        completeButton.SetActive(false);
        questCompleted = true;
        completed = false;
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
        if(completed == false)
        {
            OnQuestSuccess.Invoke();
            completed = true;
        }
    }
}
