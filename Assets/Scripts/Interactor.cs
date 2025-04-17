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
            currentInteractable = interactable;

            Debug.Log("It's interactable");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (interactable == currentInteractable) 
            {

                currentInteractable = null;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }
    private Interactable GetPriorityInteractable() 
    {
        float minDistance = Vector2.Distance(transform.position, interactables[0].transform.position);
        Interactable priorityInteractable = interactables[0];
        foreach(var interactable in interactables ) 
        {
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
