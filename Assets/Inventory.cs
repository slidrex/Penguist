using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] public Item[] Items;
    [SerializeField] private Entity holder;
    [SerializeField] private InventoryItemName nameDisplayer;
    private Collider2D Collider;
    private InventorySlot[] slots;
    public System.Action OnInventoryChanged;
    [SerializeField] private CollectableItem collectableItem;
    private int selectedSlot;
    private Item previousFrameSelectedItem;
    private void Start()
    {
        InitSlots();
        Collider = holder.GetComponent<Collider2D>();
        OnInventoryChanged += RenderSlots;
        for(int i = 0; i < Items.Length; i++) if(Items[i] != null) 
        {
            Items[i] = Instantiate(Items[i]);
            Items[i].OnAttach(holder);
        }
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
    public void Remove(Item item)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if(Items[i] != null && Items[i].Name == item.Name)
            {
                Items[i] = null;
                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }
    public bool IsFull()
    {
        foreach(Item item in Items)
        {
            if(item == null) return false;
        }
        return true;
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
        Item curItem = Items[selectedSlot];
        Vector2 mouseScrollDelta = Input.mouseScrollDelta;
        if(mouseScrollDelta != Vector2.zero)
        {
            Select((int)mouseScrollDelta.y);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(selectedSlot);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1) && Items[selectedSlot] != null)
        {
            Items[selectedSlot].OnItemSecondaryUse(holder);
        }
        if(previousFrameSelectedItem != curItem && Items[selectedSlot] != null)
        {
            nameDisplayer.EnableName(Items[selectedSlot].Name);
        }
        previousFrameSelectedItem = curItem;
        if(Input.GetKeyDown(KeyCode.Alpha1)) SelectIndex(0);
        if(Input.GetKeyDown(KeyCode.Alpha2)) SelectIndex(1);
        if(Input.GetKeyDown(KeyCode.Alpha3)) SelectIndex(2);
        if(Input.GetKeyDown(KeyCode.Alpha4)) SelectIndex(3);
        if(Input.GetKeyDown(KeyCode.Alpha5)) SelectIndex(4);
    }
    public bool ContainsItem(string name)
    {
        foreach(Item curItem in Items)
        {
            if(curItem != null && curItem.Name == name) return true;
        }
        print("False");
        return false;
    }
    public bool ContainsItem(Item item)
    {
        foreach(Item curItem in Items)
        {
            if(curItem?.Name == item.Name) return true;
        }
        return false;
    }
    public int ItemCount(Item item)
    {
        int count = 0;
        foreach(Item curItem in Items)
        {
            if(curItem?.Name == item.Name) count++;
        }
        return count;
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
                Instantiatetem(i, item);
                return true;
            }
        }
        DropItem(selectedSlot);
        Instantiatetem(selectedSlot, item);
        return true;
    }
    private void Instantiatetem(int index, Item item)
    {
        Items[index] = Instantiate(item);
        Items[index].OnAttach(holder);
        OnInventoryChanged.Invoke();
    }
    public void DropItem(int index)
    {
        
        if(Items[index] != null) 
        {
            if(Items[index].DestroyOnDrop == false)
            {
                CollectableItem item = Instantiate(collectableItem, holder.transform.position, Quaternion.identity);
                item.AttachItem(Instantiate(Items[index]));
            }
            else if(Items[index].DestroyEffect != null) Destroy(Instantiate(Items[index].DestroyEffect, holder.transform.position, Quaternion.identity), 5.0f);
            Items[index].OnDrop();
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
