using UnityEngine;

public class Bottle : CollectableItem
{
    protected override void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Item = itemDatabase.GetItem(2);
        Renderer.sprite = Item.Sprite;
    }
}
