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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyCounter.text = Convert.ToString(playerInventory.money);
    }
    private void DescribeEvent(string description) 
    {
    }
}
