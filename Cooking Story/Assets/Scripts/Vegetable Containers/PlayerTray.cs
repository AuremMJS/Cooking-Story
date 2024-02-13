using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTray : MonoBehaviour, IVegetableContainer
{
    Queue<Vegetable> vegetableQueue;
    int maxVegetables;
    public bool IsHoldingSalad { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        IsHoldingSalad = false;
        SetMaxVegetables(2);
        vegetableQueue = new Queue<Vegetable>();
    }

    protected void SetMaxVegetables(int _maxVegetables)
    {
        maxVegetables = _maxVegetables;
    }

    public virtual List<Vegetable> TakeFromContainer()
    {
        if (vegetableQueue.Count == 0)
        {
            return null;
        }
        int noOfVegetablesToTake = IsHoldingSalad ? vegetableQueue.Count : 1;

        List<Vegetable> vegetables = new List<Vegetable>();
        int noOfVegetablesTaken = 0;

        while (noOfVegetablesTaken < noOfVegetablesToTake)
        {
            vegetables.Add(vegetableQueue.Dequeue());
            noOfVegetablesTaken++;
        }

        return vegetables;
    }

    public virtual bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        if(!CanTakeVegetables(vegetables.Count))
        {
            return false;
        }
        foreach (var vegetable in vegetables)
        {
            vegetableQueue.Enqueue(vegetable);
        }
        return true;
    }

    public bool HoldSalad()
    {
        if (vegetableQueue.Count == 0)
        {
            IsHoldingSalad = true;
            return true;
        }
        return false;
    }

    public bool CanTakeVegetables(int newVegetablesCount)
    {
        return ((vegetableQueue.Count < maxVegetables && (vegetableQueue.Count + newVegetablesCount) <= maxVegetables) || IsHoldingSalad);
    }

    public virtual int GetNoOfVegetablesToTake()
    {
        return IsHoldingSalad ? vegetableQueue.Count : 1;
    }
}