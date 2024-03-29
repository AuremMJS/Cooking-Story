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
        UIManager.Instance.PrintText("Thrown the salad in trash...");
        for (int i=0; i<vegetables.Count; i++)
        {
            vegetables[i] = null;
        }
        vegetables.Clear();
        vegetables = null;
        _player.IsHoldingSalad = false;
        ScoreManager.Instance[_player.GetPlayerIndex()] -= GameController.GameConstants.TRASH_PENALTY;
        return true;
    }

    // Only salad can be thrown
    public override bool CanPlaceVegetables(int newVegetablesCount)
    {
        return _player.IsHoldingSalad;
    }
}
