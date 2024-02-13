using System;
using System.Collections.Generic;

public class Customer
{
    private bool isAngry;
    public List<Vegetable> Order { get; set; }

    public Customer() 
    { 
        // Random logic to generate the order
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
            // Calculating waiting time based on the no of items and if customer is angry
            return Order.Count * GameController.GameConstants.TIME_FOR_ONE_ORDER * 
                (isAngry ? GameController.GameConstants.ANGRY_TIME_MULTIPLIER : GameController.GameConstants.NORMAL_TIME_MULTIPLIER);
        }
    }

    // Checking if the order is correct
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

    // Marking the customer angry
    public void SetAngry()
    {
        isAngry = true;
    }

    // Penalty for not providing order
    public int GetPenalty()
    {
        return GameController.GameConstants.CUSTOMER_PENALTY;
    }
}
