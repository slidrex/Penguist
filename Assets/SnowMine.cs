using UnityEngine;

public class SnowMine : HoldingInteractableObject
{
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private Item snow;
    [SerializeField] private int MaxInteractCount;
    private int currentInteractCount;
    public override void OnUnfreeze()
    {
        base.OnUnfreeze();
        MaxInteractTime = Random.Range(1, 2.0f);
        MaxInteractCount = Random.Range(3, 5);
    }
    protected override void OnInteractSuccess()
    {
        Interactor.Inventory.AddItem(snow);
        collectSound.Play();
        if(MaxInteractCount > 0)
        {
            if(currentInteractCount < MaxInteractCount)
                currentInteractCount++;
            else Destroy(gameObject);
        }
    }
}
