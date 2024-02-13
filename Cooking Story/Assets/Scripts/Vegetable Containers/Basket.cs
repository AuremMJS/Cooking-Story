using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : VegetableContainer
{
    [SerializeField]
    private VegetableType vegetableType = VegetableType.Tomato;
    [SerializeField]
    private SpriteRenderer vegetableSprite;

    // Start is called before the first frame update
    public override void Start()
    {
        int quantity = UnityEngine.Random.Range(GameController.GameConstants.MIN_ITEMS_IN_BASKET, GameController.GameConstants.MAX_ITEMS_IN_BASKET);
        SetMaxVegetables(quantity);
        for (int i = 0; i < quantity; i++)
        {
            vegetableQueue.Enqueue(new Vegetable(vegetableType,false));
        }
        base.Start();
    }

    public override List<Vegetable> TakeFromContainer()
    {
        Debug.Log($"Item{vegetableType} taken from Basket");
        return base.TakeFromContainer();
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetable)
    {
        // Player cannot place vegetables back
        return false;
    }

    protected override void UpdateVegetableSprites()
    {
        vegetableSprite.sprite = SpriteLoader.Instance.GetSpriteForVegetable(vegetableType);
    }
}
