using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private bool CanInteract = false;
    private Interactable currentInteractable;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("It's interactable");
            CanInteract = true;
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
                Debug.Log("item deleted");
                CanInteract = false;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract) 
        {
            currentInteractable.Interact();
        }
    }
}
