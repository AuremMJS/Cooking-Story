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

    // Plate is exclusive to each player
    protected override void TransferVegetables(IVegetableContainer source, IVegetableContainer destination)
    {
        if (_player.GetPlayerIndex() != playerIndex)
            return;

        base.TransferVegetables(source, destination);
    }
}
