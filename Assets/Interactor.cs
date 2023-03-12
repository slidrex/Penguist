using UnityEngine;
using System.Collections.Generic;

public class Interactor : MonoBehaviour
{
    public PlayerClueHint ClueHint;
    private List<InteractableObject> objects = new List<InteractableObject>();
    [SerializeField] private float interactDistance;
    [SerializeField] private InteractHint hint;
    [HideInInspector] public InteractableObject interact;
    public Inventory Inventory;
    private bool interactKeyPressedDown;
    public Entity entity;
    private Movement movement;
    private void Awake()
    {
        Inventory = GetComponent<InventoryHolder>().Inventory;
        movement = GetComponent<Movement>();
    }
    private void Update()
    {
        HandleNewInteractableObjects();
        
        if(interact != null) InteractInput();
        
        HandleHint();
    }
    private void HandleNewInteractableObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent<InteractableObject>(out InteractableObject obj))
            {
                if(objects.Contains(obj) == false)
                {
                    obj.Collider = collider;
                    obj.OnInteractorInRange(this);
                    objects.Add(obj);
                }
            }
        }
        RemoveInvalidObjects(colliders);
        AssignInteractObject();
    }
    private void RemoveInvalidObjects(Collider2D[] colliders)
    {
        for(int i = 0; i < objects.Count; i++)
        {
            if(objects[i] == null) 
            {
                objects.RemoveAt(i);
                return;
            }
            bool contains = false;
            foreach(Collider2D collider in colliders)
            {
                if(collider == objects[i].Collider)
                {
                    contains = true;
                    break;
                }
            }
            if(contains == false)
            {
                if(objects[i] == interact) {
                    interact = null;
                }
                objects[i].OnInteractorOutOfRange();
                objects.RemoveAt(i);
            }
        }
    }
    private void AssignInteractObject()
    {
        if(interact == null && objects.Count > 0 && entity.ContainsRule(Entity.Rule.DisableInteraction) == false)
        {
            for(int i = 0; i < objects.Count; i++)
            {
                if(objects[i].IsInteractable(this)) 
                {
                    objects[i].SendInteractRequest(this);
                    interact = objects[i];
                    interactKeyPressedDown = false;
                    RenderInteractHint(true);
                    return;
                }
            }
        }
    }
    public void RenderInteractHint(bool render)
    {
        hint.gameObject.SetActive(render);
        if(render)
        {
            hint.HintText.text = interact.InteractString + " [E]";

        }
    }
    private void InteractInput()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            interactKeyPressedDown = true;
            float facing = interact.transform.position.x - transform.position.x;
            movement.SetFacing(Mathf.Sign(facing) * Vector2.right);
            interact.OnInteractKeyDown();
        }
        
        if(Input.GetKeyUp(KeyCode.E) && interactKeyPressedDown)
        {
            interact.OnInteractKeyUp();
            interactKeyPressedDown = false;
        }

    }
    public void InterruptInteraction()
    {
        objects.Remove(interact);
        interact.InteractEnd();
        interact.Interactor = null;
        interact = null;
    }
    private void HandleHint()
    {
        bool disabled = entity.ContainsRule(Entity.Rule.DisableInteraction);
        if((interact == null || disabled) && hint.gameObject.activeSelf) RenderInteractHint(false);
        else if(interact != null && disabled == false) RenderInteractHint(true);
    }
}
