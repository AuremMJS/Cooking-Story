using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVegetableContainer
{
    List<Vegetable> TakeFromContainer();
    bool PlaceIntoContainer(List<Vegetable> vegetable);
    bool CanPlaceVegetables(int newVegetablesCount);
    int GetNoOfVegetablesToTake();
}

public abstract class VegetableContainer : MonoBehaviour, IVegetableContainer
{
    [SerializeField]
    protected SpriteRenderer[] vegetableSprites;

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
        UpdateVegetableSprites();
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
        UpdateVegetableSprites();
        return vegetables;
    }

    public virtual int GetNoOfVegetablesToTake()
    {
        return GameController.GameConstants.DEFAULT_NO_OF_ITEMS_TO_TAKE;
    }
    
    public virtual bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        if(!CanPlaceVegetables(vegetables.Count))
        {
            return false;
        }
        foreach (var vegetable in vegetables)
        {
            vegetableQueue.Enqueue(vegetable);
        }
        UpdateVegetableSprites();
        return true;
    }
    private void OnItemTaken(int index)
    {
        if(_player != null && index == _player.GetPlayerIndex())
            TransferVegetables(this, _player);
    }

    private void OnItemPlaced(int index)
    {
        if(_player != null && index == _player.GetPlayerIndex())
            TransferVegetables(_player, this);
    }

    protected virtual void TransferVegetables(IVegetableContainer source, IVegetableContainer destination)
    {
        if (source != null && destination != null && destination.CanPlaceVegetables(source.GetNoOfVegetablesToTake()))
        {
            List<Vegetable> vegetables = source.TakeFromContainer();
            if (vegetables != null)
                destination.PlaceIntoContainer(vegetables);
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

    public virtual bool CanPlaceVegetables(int newVegetablesCount)
    {
        return (vegetableQueue.Count < maxVegetables && (vegetableQueue.Count + newVegetablesCount) <= maxVegetables);
    }

    protected virtual void UpdateVegetableSprites()
    {
        int i = 0;
        foreach (Vegetable vegetable in vegetableQueue)
        {
            vegetableSprites[i].gameObject.SetActive(true);
            vegetableSprites[i].sprite = SpriteLoader.Instance.GetSpriteForVegetable(vegetable.Type);
            i++;
        }

        for (; i < vegetableSprites.Length; i++)
        {
            vegetableSprites[i].gameObject.SetActive(false);
        }
    }
}
