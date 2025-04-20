using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Interactable currentInteractable;
    private List<Interactable> interactables;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (!interactables.Contains(interactable))
            {
                interactables.Add(interactable);
            }
            //Debug.Log("Interactable added: " + interactable.name);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (interactables.Contains(interactable))
            {
                interactables.Remove(interactable);
            }
            //Debug.Log("Interactable removed: " + interactable.name);

            if (interactable == currentInteractable)
            {
                currentInteractable = null;
            }
        }
    }
    private void Update()
    {
        currentInteractable = GetPriorityInteractable();
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }
    private void Start()
    {
        interactables = new List<Interactable>();
    }
    private Interactable GetPriorityInteractable() 
    {
        if (interactables == null || interactables.Count == 0)
        {
            return null; 
        }
        // Find the interactable with the highest priority (closest to the player)
        float minDistance = Vector2.Distance(transform.position, interactables[0].transform.position);
        Interactable priorityInteractable = interactables[0];
        foreach(var interactable in interactables ) 
        {
            if (interactable.CompareTag("Plant"))
            {
                priorityInteractable = interactable;
                return priorityInteractable;
            }
            float distance = Vector2.Distance(transform.position, interactable.transform.position);
            if(distance < minDistance)
            {
                priorityInteractable = interactable;
                minDistance = distance;
            }
        }
        return priorityInteractable;
    }
}
