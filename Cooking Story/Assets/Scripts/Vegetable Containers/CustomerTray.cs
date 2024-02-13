using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerTray : VegetableContainer
{
    static float customerQueueTime;
    float orderedTime = 0;
    Customer customer;
    bool orderPlaced = false;
    // Start is called before the first frame update
    public override void Start()
    {
        customer = new Customer();
        SetMaxVegetables(100);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!orderPlaced)
        {
            PlaceOrder();
        }
        if (customer != null && Time.time - orderedTime > customer.WaitingTime)
        {
            Debug.Log($"Customer left the restaurant {customer.WaitingTime}");
            customer = new Customer();
            orderPlaced = false;
        }
    }

    void PlaceOrder()
    {
        orderedTime = Time.time + customerQueueTime;
        customerQueueTime += customer.WaitingTime;
        orderPlaced = true;
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        if (vegetables != null && customer.IsOrderCorrect(vegetables))
        {
            Debug.Log("Order completed");
            for (int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i] = null;
            }
            vegetables.Clear();
            vegetables = null;
            return true;
        }
        else
        {
            Debug.Log("Order wrong");
            return base.PlaceIntoContainer(vegetables);
        }
    }
}
