using UnityEngine;

public class CollectableItem : InteractableObject
{
    [HideInInspector] public Rigidbody2D Rigidbody;
    private float blockTime;
    private float timeSinceBlock;
    private Item Item;
    private BlockTimer blockTimer;
    [SerializeField] private Item itemDatabase;
    public override string InteractString => "Collect";
    protected override void Awake()
    {
        base.Awake();
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Block(float blockTime, Collider2D collider)
    {
        blockTimer = new BlockTimer();
        blockTimer.Blocked.Add(collider);
        Physics2D.IgnoreCollision(collider, Collider);
        blockTimer.RemainTime = blockTime;
    }
    public override void OnInteractKeyDown()
    {
        bool success = Interactor.Inventory.AddItem(Item);
        if(success) Destroy(gameObject);
    }
    protected override void Update()
    {
        base.Update();
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
        Renderer.sprite = item.Sprite;
    }
    private class BlockTimer
    {
        public float RemainTime;
        public System.Collections.Generic.List<Collider2D> Blocked = new System.Collections.Generic.List<Collider2D>();
    }
}
