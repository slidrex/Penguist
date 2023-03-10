using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item[] Items;
    [SerializeField] private Transform holder;
    private Collider2D Collider;
    private InventorySlot[] slots;
    private System.Action OnInventoryChanged;
    [SerializeField] private CollectableItem collectableItem;
    private int selectedSlot;
    private void Awake()
    {
        InitSlots();
        Collider = holder.GetComponent<Collider2D>();
        OnInventoryChanged += RenderSlots;
        for(int i = 0; i < Items.Length; i++) if(Items[i] != null) Items[i] = Instantiate(Items[i]);
        RenderSlots();
        SelectIndex(0);
    }
    private void Select(int offset)
    {
        int newSlot = (int)Mathf.Repeat(selectedSlot - offset, slots.Length);
        slots[selectedSlot].Select(false);
        slots[newSlot].Select(true);
        selectedSlot = newSlot;
    }
    private void SelectIndex(int index)
    {
        int newSlot = index;
        slots[selectedSlot].Select(false);
        slots[newSlot].Select(true);
        selectedSlot = newSlot;
    }
    private void Update()
    {
        Vector2 mouseScrollDelta = Input.mouseScrollDelta;
        if(mouseScrollDelta != Vector2.zero)
        {
            Select((int)mouseScrollDelta.y);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(selectedSlot);
        }
    }
    private void InitSlots()
    {
        Transform slotHolder = transform;
        slots = new InventorySlot[slotHolder.childCount];
        for(int i = 0; i < slotHolder.childCount; i++)
        {
            slots[i] = slotHolder.GetChild(i).GetComponent<InventorySlot>();
        }
    }
    public bool AddItem(Item item)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == null) 
            {
                Items[i] = Instantiate(item);
                OnInventoryChanged.Invoke();
                return true;
            }
        }
        return false;
    }
    public void DropItem(int index)
    {
        if(Items[index] != null) 
        {
            CollectableItem item = Instantiate(collectableItem, holder.position, Quaternion.identity);
            item.AttachItem(Instantiate(Items[index]));
            Items[index] = null;
            OnInventoryChanged.Invoke();
        }
    }
    private void RenderSlots()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(Items[i] != null)
            {
                slots[i].RenderSlot(Items[i].Sprite);
            } else slots[i].RenderSlot(null);
        }
    }
}
