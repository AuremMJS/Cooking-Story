using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
    private bool isAngry;
    public List<Vegetable> Order { get; set; }

    public Customer() 
    { 
        int noOfItems = UnityEngine.Random.Range(GameController.GameConstants.MIN_ITEMS_IN_ORDER, GameController.GameConstants.MAX_ITEMS_IN_ORDER);
        Order = new List<Vegetable>(noOfItems);
        Array vegetables = Enum.GetValues(typeof(VegetableType));
        for (int i = 0; i < noOfItems; i++)
        {
            int randomVeg = UnityEngine.Random.Range(0, vegetables.Length);
            Order.Add(new Vegetable((VegetableType) randomVeg,true));
        }
    }
    public float WaitingTime
    {
        get
        {
            return Order.Count * GameController.GameConstants.TIME_FOR_ONE_ORDER * 
                (isAngry ? GameController.GameConstants.ANGRY_TIME_MULTIPLIER : GameController.GameConstants.NORMAL_TIME_MULTIPLIER);
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

    public void SetAngry()
    {
        isAngry = true;
    }

    public int GetPenalty()
    {
        return GameController.GameConstants.CUSTOMER_PENALTY;
    }
}
