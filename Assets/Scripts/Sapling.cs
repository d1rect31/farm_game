using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    public AdultTree tree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Convert.ToInt32(Time.time) == 5) 
     {
        Destroy(gameObject);
        Instantiate(tree, transform.position, Quaternion.identity);
     }   
    }
}
