using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed;
    Animator animComponent;
    
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animComponent = GetComponent<Animator>();
    }
    void Update()
    {   
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) 
        {
            moveVector += new Vector3(0, 1, 0);
            animComponent.SetBool("WalkUp", true);
        }
        else {animComponent.SetBool("WalkUp", false);}
        if (Input.GetKey(KeyCode.A)) 
        {
            moveVector += new Vector3(-1, 0, 0);
            animComponent.SetBool("WalkLeft", true);
        }
        else {animComponent.SetBool("WalkLeft", false);}
        if (Input.GetKey(KeyCode.S)) 
        {
            moveVector += new Vector3(0, -1, 0);
            animComponent.SetBool("WalkDown", true);
        }
        else {animComponent.SetBool("WalkDown", false);}
        if (Input.GetKey(KeyCode.D)) 
        {
            moveVector += new Vector3(1, 0, 0);
            animComponent.SetBool("WalkRight", true);
        }
        else {animComponent.SetBool("WalkRight", false);}
        moveVector.Normalize();
        rigidbody.velocity = moveVector * speed;
        if (moveVector == Vector3.zero)
        {
            animComponent.SetBool("WalkLeft", false);
            animComponent.SetBool("WalkDown", false);
            animComponent.SetBool("WalkRight", false);
            animComponent.SetBool("WalkUp", false);
        }
        // костыльный метод но ладно !!
        ///animation player
    }
}
