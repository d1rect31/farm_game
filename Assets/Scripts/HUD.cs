using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text moneyCounter;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Text eventDescriptor;
    // Start is called before the first frame update
    float timer;
    float normaltime;
    bool counting;
    void Start()
    {
        counting = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        moneyCounter.text = Convert.ToString(playerInventory.money);
        if (counting) 
        {
            timer -= Time.deltaTime;
            if (timer < -3) 
            {
                eventDescriptor.text = "";
                counting = false;
            }
        // Debug.Log(timer);
        }
        else {timer = normaltime; counting = false;}
    }
    public void DescribeEvent(string desc) 
    {
        eventDescriptor.text = desc;
        counting = true;
    }
}
