using UnityEngine;

[CreateAssetMenu(menuName = "Penguin/Revolver")]
public class FrozenRevolver : Weapon
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Item ammoObject;
    private Inventory inventory;
    private PlayerClueHint clue;
    public override void OnItemSecondaryUse(Entity entity)
    {
        if(inventory.ContainsItem(ammoObject))
        {
            Shoot();
            inventory.Remove(ammoObject);
        } else
        {
            clue.CreateHint("...");
        }
    }
    private void Shoot()
    {
        Vector2 shootDirection = ((Vector3)PenguiFunctions.GetMousePosition() - User.transform.position).normalized;
        var bul = Instantiate(bullet, User.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), User.GetComponent<Collider2D>());
        bul.MoveDirection = shootDirection;
        bul.BulletSpeed = 6.0f;
    }
    public override void OnAttach(Entity entity)
    {
        base.OnAttach(entity);
        clue = (entity as IHintHolder).Hint;
        inventory = entity.GetComponent<InventoryHolder>().Inventory;
    }
    public override void OnDrop()
    {
        base.OnDrop();
        inventory = null;
    }
}
