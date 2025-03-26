using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {   
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) 
        {
            moveVector += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            moveVector += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            moveVector += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            moveVector += new Vector3(1, 0, 0);
        }
        moveVector.Normalize();
        transform.position = transform.position + moveVector * speed * Time.deltaTime;
    }
}
