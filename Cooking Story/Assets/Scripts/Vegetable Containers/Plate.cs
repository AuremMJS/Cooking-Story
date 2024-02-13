using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : VegetableContainer
{
    // Start is called before the first frame update
    public void Start()
    {
        SetMaxVegetables(1);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
