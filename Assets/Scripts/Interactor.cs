using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Interactable currentInteractable;
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
}
