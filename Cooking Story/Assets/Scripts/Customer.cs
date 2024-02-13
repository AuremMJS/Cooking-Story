using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
    static float customerQueueTime;
    public List<Vegetable> Order { get; set; }

    public Customer() 
    { 
        int noOfItems = UnityEngine.Random.Range(1, 3);
        Order = new List<Vegetable>(noOfItems);
        Array vegetables = Enum.GetValues(typeof(VegetableType));
        Debug.Log("Customer Order :");
        for (int i = 0; i < noOfItems; i++)
        {
            int randomVeg = UnityEngine.Random.Range(0, vegetables.Length);
            Order.Add(new Vegetable((VegetableType) randomVeg,false));
            Debug.Log(Order[i].Type.ToString());
        }
    }
    public float WaitingTime
    {
        get
        {
            return Order.Count * 15.0f;
        }
    }

    public bool IsOrderCorrect(List<Vegetable> salad)
    {
        int noOfItemsMatching = 0;
        foreach (var vegetable in Order)
        {
            if(salad.Contains(vegetable))
                noOfItemsMatching++;
        }
        return noOfItemsMatching == Order.Count;
    }

}
