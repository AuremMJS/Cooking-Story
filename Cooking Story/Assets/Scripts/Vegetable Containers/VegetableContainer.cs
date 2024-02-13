using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVegetableContainer
{
    List<Vegetable> TakeFromContainer();
    bool PlaceIntoContainer(List<Vegetable> vegetable);
    bool CanTakeVegetables(int newVegetablesCount);
    int GetNoOfVegetablesToTake();
}

public abstract class VegetableContainer : MonoBehaviour, IVegetableContainer
{
    private int maxVegetables;
    protected PlayerTray _player;
    protected Queue<Vegetable> vegetableQueue;

    void Awake()
    {
        vegetableQueue = new Queue<Vegetable>();

        _player = null;
    }

    public virtual void Start()
    {
        InputController.Instance.ItemTaken += OnItemTaken;
        InputController.Instance.ItemPlaced += OnItemPlaced;
    }


    protected void SetMaxVegetables(int _maxVegetables)
    {
        maxVegetables = _maxVegetables;
    }

    public virtual List<Vegetable> TakeFromContainer()
    {
        if(vegetableQueue.Count == 0)
        {
            return null;
        }

        List<Vegetable> vegetables = new List<Vegetable>();
        vegetables.Add(vegetableQueue.Dequeue());
        return vegetables;
    }

    public virtual int GetNoOfVegetablesToTake()
    {
        return 1;
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
    private void OnItemTaken()
    {
        if (_player != null && _player.CanTakeVegetables(GetNoOfVegetablesToTake()))
        {
            List<Vegetable> vegetables = TakeFromContainer();
            if(vegetables!= null)
                _player?.PlaceIntoContainer(vegetables);
        }
    }

    private void OnItemPlaced()
    {
        if (_player != null && CanTakeVegetables(_player.GetNoOfVegetablesToTake()))
        {
            List<Vegetable> vegetables = _player?.TakeFromContainer();
            PlaceIntoContainer(vegetables);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _player = other.GetComponent<PlayerTray>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _player = null;
    }

    public bool CanTakeVegetables(int newVegetablesCount)
    {
        return (vegetableQueue.Count < maxVegetables && (vegetableQueue.Count + newVegetablesCount) <= maxVegetables);
    }
}
