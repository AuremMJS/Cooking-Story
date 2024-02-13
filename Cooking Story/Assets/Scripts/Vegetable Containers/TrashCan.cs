using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : VegetableContainer
{
    // Start is called before the first frame update
    public override void Start()
    {
        SetMaxVegetables(1000);
        base.Start();
    }

    public override List<Vegetable> TakeFromContainer()
    {
        // We cannot take from Trash
        return null;
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        // We can throw only salad into trash
        if(!_player.IsHoldingSalad)
            return false;
        Debug.Log("Throwing into trash");
        for (int i=0; i<vegetables.Count; i++)
        {
            vegetables[i] = null;
        }
        vegetables.Clear();
        vegetables = null;
        _player.IsHoldingSalad = false;
        return true;
    }

    public override bool CanPlaceVegetables(int newVegetablesCount)
    {
        return _player.IsHoldingSalad;
    }
}
