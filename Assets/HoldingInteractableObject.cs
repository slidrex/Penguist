using UnityEngine;

public class HoldingInteractableObject : InteractableObject
{
    [SerializeField] private ProgressIndicator indicator;
    private ProgressIndicator _indicator;
    public override string InteractString => "Взаимодействовать";
    private bool interactStart;
    protected float InteractValue;
    [SerializeField] protected float MaxInteractTime;
    protected override void Update()
    {
        base.Update();
        if(interactStart) OnInteractLoop();
    }
    public override void OnInteractKeyDown()
    {
        interactStart = true;
        BeginInteractLoop();
    }
    protected void BeginInteractLoop()
    {
        InteractValue = 0.0f;
        this.Interactor.entity.AddRule(Entity.Rule.DisableMovement);
        this.Interactor.entity.AddRule(Entity.Rule.DisableInteraction);
        _indicator = Instantiate(indicator, Interactor.entity.transform.position + Vector3.up, Quaternion.identity, (Interactor.entity as ICanvasHolder).FollowCanvas.transform);
        print("Holding start");
    }
    private void OnInteractLoop()
    {
        _indicator.SetValue(InteractValue, MaxInteractTime);
        if(InteractValue >= MaxInteractTime)
        {
            Destroy(_indicator);
            OnInteractSuccess();
            Interactor?.InterruptInteraction();
        }
        else
            InteractValue += Time.deltaTime;
    }
    protected virtual void OnInteractSuccess()
    {

    }
    public override void OnInteractKeyUp()
    {
        Interactor.InterruptInteraction();
    }
    protected override void OnInteractEnd()
    {
        interactStart = false;
        InteractValue = 0.0f;
        print("Holding end");
        Destroy(_indicator.gameObject);
        this.Interactor.entity.RemoveRule(Entity.Rule.DisableMovement);
        this.Interactor.entity.RemoveRule(Entity.Rule.DisableInteraction);
    }
}
