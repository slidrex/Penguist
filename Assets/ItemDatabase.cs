using UnityEngine;

[CreateAssetMenu(menuName = "Penguin/Unfrozen Items Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private Item[] Items;
    private int offset;
    public void Reset()
    {
        offset = 0;
    }
    public Item GetItem() 
    {
        Item item = Items[offset];
        offset++;
        return item;
    }
}
