using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private float X;
    [SerializeField] private float Y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Interact()
    {
        var player = GameObject.Find("Player");
        var teleportpos = new Vector2(X, Y);
        player.transform.position = teleportpos;
        Debug.Log("teleporting");
    }
}
