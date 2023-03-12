using UnityEngine;

public class FrozenDoor : FrozenObject
{
    [SerializeField] private HouseEntry entry;

    public override void OnUnfreeze()
    {
        entry.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
