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
        foreach (var interactable in interactables)
        {
            float distance = Vector2.Distance(transform.position, interactable.transform.position);
            // If the interactable is a Collectable, give it priority
            if (interactable.CompareTag("Collectable"))
            {
                if(distance < minDistance)
                {
                    priorityInteractable = interactable;
                    minDistance = distance;
                }
            }
        }
        // If no Collectable interactable is found, find the closest one
        if (priorityInteractable == null)
        {
            foreach (var interactable in interactables)
            {
                float distance = Vector2.Distance(transform.position, interactable.transform.position);
                if(distance < minDistance)
                {
                    priorityInteractable = interactable;
                    minDistance = distance;
                }
            }
        }
        return priorityInteractable;
    }
}
