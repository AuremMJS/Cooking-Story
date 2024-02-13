using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ChoppingBoard : VegetableContainer
{
    [SerializeField]
    private int playerIndex;

    float choppingTime;
    bool choppingInProgress;
    float choppingStartTime;

    // Start is called before the first frame update
    public override void Start()
    {
        choppingInProgress = false;
        choppingStartTime = 0;
        choppingTime = GameController.GameConstants.CHOPPING_TIME;
        SetMaxVegetables(100);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if chopping is done
        if(choppingInProgress && Time.time - choppingStartTime > choppingTime)
        {
            choppingInProgress =false;
            _player.GetComponent<Player>().ResetCurrentSpeed();
            UIManager.Instance.PrintText("Chopping Done...");
        }
    }

    public override List<Vegetable> TakeFromContainer()
    {
        // Taking the salad from the chopping board
        if (_player.HoldSalad())
        {
            UIManager.Instance.PrintText("Salad taken from board...");
            List<Vegetable> vegetables = vegetableQueue.ToList<Vegetable>();
            vegetableQueue.Clear();
            UpdateVegetableSprites();
            return vegetables;
        }
        return null;
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        // Do not place for chopping when anoter vegetable is already chopping
        if(vegetables == null || vegetables.Count == 0 || choppingInProgress) return false;

        // If holding a salad, keeping it on chopping board
        if (_player.IsHoldingSalad)
        {
            _player.IsHoldingSalad = false;
        }
        else
        {
            // Chopping a vegetable. Player speed is 0
            UIManager.Instance.PrintText("Chopping...");
            _player.GetComponent<Player>().CurrentSpeed = 0;
            choppingStartTime = Time.time;
            foreach (var vegetable in vegetables)
            {
                vegetable.IsChopped = true;
            }
        }
        bool isVegetablesPlaced = base.PlaceIntoContainer(vegetables);
        choppingInProgress = !_player.IsHoldingSalad && isVegetablesPlaced;
        return isVegetablesPlaced;
    }

    // Is the board ready to place some vegetables
    public override bool CanPlaceVegetables(int newVegetablesCount)
    {
        return !choppingInProgress && base.CanPlaceVegetables(newVegetablesCount);
    }

    // Chopping board is exclusive to each player
    protected override void TransferVegetables(IVegetableContainer source, IVegetableContainer destination)
    {
        if (_player.GetPlayerIndex() != playerIndex)
            return;

        base.TransferVegetables(source, destination);
    }
}
