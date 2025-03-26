using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    Animator animComponent;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
            animComponent = GetComponent<Animator>();
    }
    void Update()
    {   
        animComponent.SetBool("WalkLeft", false);
        animComponent.SetBool("WalkDown", false);
        animComponent.SetBool("WalkRight", false);
        animComponent.SetBool("WalkUp", false);
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) 
        {
            moveVector += new Vector3(0, 1, 0);
            animComponent.SetBool("WalkUp", true);
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            moveVector += new Vector3(-1, 0, 0);
            animComponent.SetBool("WalkLeft", true);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            moveVector += new Vector3(0, -1, 0);
            animComponent.SetBool("WalkDown", true);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            moveVector += new Vector3(1, 0, 0);
            animComponent.SetBool("WalkRight", true);
        }
        moveVector.Normalize();
        transform.position = transform.position + moveVector * speed * Time.deltaTime;


        ///animation player
    }
}
