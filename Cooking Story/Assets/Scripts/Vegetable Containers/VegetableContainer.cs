using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVegetableContainer
{
    
    List<Vegetable> TakeFromContainer();
    bool PlaceIntoContainer(List<Vegetable> vegetable);
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

    public void Start()
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
    
    public virtual bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        if(vegetableQueue.Count >= maxVegetables || (vegetableQueue.Count + vegetables.Count) > maxVegetables)
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
        if (_player != null && !_player.IsHoldingSalad)
        {
            List<Vegetable> vegetables = TakeFromContainer();
            if(vegetables!= null)
                _player?.PlaceIntoContainer(vegetables);
        }
    }

    private void OnItemPlaced()
    {
        if (_player != null && vegetableQueue.Count < maxVegetables)
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
}
