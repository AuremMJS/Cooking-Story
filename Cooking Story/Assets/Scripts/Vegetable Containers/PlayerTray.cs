using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTray : MonoBehaviour, IVegetableContainer
{
    [SerializeField]
    private SpriteRenderer[] vegetableSprites;

    Player player;
    Queue<Vegetable> vegetableQueue;
    int maxVegetables;
    public bool IsHoldingSalad { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        IsHoldingSalad = false;
        SetMaxVegetables(GameController.GameConstants.MAX_ITEMS_IN_PLAYER_TRAY);
        vegetableQueue = new Queue<Vegetable>();
        UpdateVegetableSprites();
    }

    protected void SetMaxVegetables(int _maxVegetables)
    {
        maxVegetables = _maxVegetables;
    }

    public virtual List<Vegetable> TakeFromContainer()
    {
        if (vegetableQueue.Count == 0)
        {
            return null;
        }
        
        // Player can place more vegetables if they are part of salad
        List<Vegetable> vegetables = IsHoldingSalad ? vegetableQueue.ToList<Vegetable>() : new List<Vegetable>() { vegetableQueue.Dequeue() };
        if(IsHoldingSalad) vegetableQueue.Clear();
        UpdateVegetableSprites();
        return vegetables;
    }

    // Taking the vegetables into the player
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

    // Hold the salad if the tray is empty
    public bool HoldSalad()
    {
        if (vegetableQueue.Count == 0)
        {
            IsHoldingSalad = true;
            return true;
        }
        return false;
    }

    // Checking if vegetables can be placed in the tray
    public bool CanPlaceVegetables(int newVegetablesCount)
    {
        return ((vegetableQueue.Count < maxVegetables && (vegetableQueue.Count + newVegetablesCount) <= maxVegetables) || IsHoldingSalad);
    }

    // If salad, take all the vegetables
    public virtual int GetNoOfVegetablesToTake()
    {
        return IsHoldingSalad ? vegetableQueue.Count : 1;
    }

    // Updating sprites
    private void UpdateVegetableSprites()
    {
        int i = 0;
        foreach(Vegetable vegetable in vegetableQueue)
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

    // Get player index
    public int GetPlayerIndex()
    {
        return player.PlayerIndex;
    }

    // Get player data
    public Player GetPlayer()
    {
        return player;
    }
}