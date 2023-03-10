using UnityEngine;

public class CollectableItem : InteractableObject
{
    private SpriteRenderer _renderer;
    [HideInInspector] public Rigidbody2D Rigidbody;
    private float blockTime;
    private float timeSinceBlock;
    private Item Item;
    private BlockTimer blockTimer;
    private Collider2D Collider;

    public override string InteractString => "Collect";
    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void Block(float blockTime, Collider2D collider)
    {
        blockTimer = new BlockTimer();
        blockTimer.Blocked.Add(collider);
        Physics2D.IgnoreCollision(collider, Collider);
        blockTimer.RemainTime = blockTime;
    }
    public override void OnInteract()
    {
        bool success = Interactor.GetComponent<InventoryHolder>().Inventory.AddItem(Item);
        if(success) Destroy(gameObject);
    }
    private void Update()
    {
        if(blockTimer != null)
        {
            if(blockTimer.RemainTime > 0) blockTimer.RemainTime -= Time.deltaTime;
            else 
            {
                foreach(Collider2D collider in blockTimer.Blocked)
                {
                    Physics2D.IgnoreCollision(collider, Collider, false);
                }
                blockTimer = null;}
        }
    }
    public void AttachItem(Item item)
    {
        Item = item;
        _renderer.sprite = item.Sprite;
    }
    private class BlockTimer
    {
        public float RemainTime;
        public System.Collections.Generic.List<Collider2D> Blocked = new System.Collections.Generic.List<Collider2D>();
    }
}
