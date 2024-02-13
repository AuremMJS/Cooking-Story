using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ChoppingBoard : VegetableContainer
{
    float choppingTime = 5.0f;
    bool choppingInProgress;
    float choppingStartTime;

    // Start is called before the first frame update
    public override void Start()
    {
        choppingInProgress = false;
        choppingStartTime = 0;
        SetMaxVegetables(100);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(choppingInProgress && Time.time - choppingStartTime > choppingTime)
        {
            choppingInProgress =false;
            InputController.Instance.SetPlayerSpeed(10.0f);
            Debug.Log("Chopping done");
        }
    }

    public override List<Vegetable> TakeFromContainer()
    {
        if (_player.HoldSalad())
        {
            Debug.Log($"Salad taken from board");
            List<Vegetable> vegetables = vegetableQueue.ToList<Vegetable>();
            vegetableQueue.Clear();
            UpdateVegetableSprites();
            return vegetables;
        }
        return null;
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        if(vegetables == null || vegetables.Count == 0 || choppingInProgress) return false;

        if (_player.IsHoldingSalad)
        {
            Debug.Log($"Salad placed into board");
            _player.IsHoldingSalad = false;
        }
        else
        {
            Debug.Log($"{vegetables[0]?.Type} placed into board");
            InputController.Instance.SetPlayerSpeed(0);
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

    public override bool CanPlaceVegetables(int newVegetablesCount)
    {
        return !choppingInProgress && base.CanPlaceVegetables(newVegetablesCount);
    }
}
