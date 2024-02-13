using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : VegetableContainer
{
    [SerializeField]
    private int playerIndex;

    // Start is called before the first frame update
    public override void Start()
    {
        SetMaxVegetables(1);
        base.Start();
    }

    public override List<Vegetable> TakeFromContainer()
    {
        Debug.Log($"Taken {vegetableQueue.Peek()?.Type} from Plate");
        return base.TakeFromContainer();
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        Debug.Log($"Placed {vegetables[0]?.Type} into Plate");
        return base.PlaceIntoContainer(vegetables);
    }

    protected override void TransferVegetables(IVegetableContainer source, IVegetableContainer destination)
    {
        if (_player.GetPlayerIndex() != playerIndex)
            return;

        base.TransferVegetables(source, destination);
    }
}
