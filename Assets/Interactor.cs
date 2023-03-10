using UnityEngine;
using System.Collections.Generic;

public class Interactor : MonoBehaviour
{
    private List<InteractableObject> objects = new List<InteractableObject>();
    [SerializeField] private float interactDistance;
    [SerializeField] private InteractHint hint;
    private InteractableObject interact;
    private void Update()
    {
        HandleInteractableObjects();
        InteractInput();
        HandleHint();
    }
    private void HandleInteractableObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent<InteractableObject>(out InteractableObject obj))
            {
                if(objects.Contains(obj) == false && obj.SendInteractRequest(this))
                {
                    objects.Add(obj);
                    bool request = obj.SendInteractRequest(this);
                    interact = obj;
                    if(request) 
                    {
                        RenderInteractHint();
                    }
                }
            }
        }
        for(int i = 0; i < objects.Count; i++)
        {
            if(objects[i] == null) 
            {
                objects.RemoveAt(i);
                return;
            }
            if((objects[i].transform.position - transform.position).sqrMagnitude > interactDistance * interactDistance) 
            {
                if(objects[i] == interact) 
                {
                    interact = null;}
                objects[i].InteractEnd();
                objects.RemoveAt(i);
            }
        }
        if(interact == null && objects.Count > 0)
        {
            for(int i = 0; i < objects.Count; i++)
            {
                if(objects[i].SendInteractRequest(this)) 
                {
                    interact = objects[i];
                    RenderInteractHint();
                }
            }
        }
    }
    public void RenderInteractHint()
    {
        hint.gameObject.SetActive(interact != null);
        if(interact != null)
        {
            hint.HintText.text = interact.InteractString + " (E)";

        }
    }
    private void InteractInput()
    {
        if(Input.GetKeyDown(KeyCode.E) && interact != null)
        {
            interact.OnInteract();
        }

    }
    private void HandleHint()
    {
        if(interact == null && hint.gameObject.activeSelf) hint.gameObject.SetActive(false);
    }
}
