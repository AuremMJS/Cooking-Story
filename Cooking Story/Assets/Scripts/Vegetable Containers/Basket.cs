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
        // Generate many vegetables of vegetable type
        int quantity = UnityEngine.Random.Range(GameController.GameConstants.MIN_ITEMS_IN_BASKET, GameController.GameConstants.MAX_ITEMS_IN_BASKET);
        SetMaxVegetables(quantity);
        for (int i = 0; i < quantity; i++)
        {
            vegetableQueue.Enqueue(new Vegetable(vegetableType,false));
        }
        base.Start();
    }

    public override bool PlaceIntoContainer(List<Vegetable> vegetable)
    {
        // Player cannot place vegetables back
        return false;
    }

    protected override void UpdateVegetableSprites()
    {
        // Update the sprite for based on the type
        vegetableSprite.sprite = SpriteLoader.Instance.GetSpriteForVegetable(vegetableType);
    }
}
