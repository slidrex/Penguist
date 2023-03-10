using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Image selectIcon;
    public void RenderSlot(Sprite image)
    {
        slotImage.gameObject.SetActive(image != null);
        slotImage.sprite = image;
    }
    public void Select(bool select)
    {
        selectIcon.gameObject.SetActive(select);
    }
}
