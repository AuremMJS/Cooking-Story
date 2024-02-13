using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTray : VegetableContainer
{
    [SerializeField]
    public Slider waitTimeProgressBar;

    float orderedTime = 0;
    Customer customer;
    bool orderPlaced = false;
    int angryIndex = -1;
    float waitTimePercentageRemaining = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        // New customer
        ResetToNewCustomer();
        SetMaxVegetables(100);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Update progress bar
        waitTimePercentageRemaining = (customer.WaitingTime - (Time.time - orderedTime)) / customer.WaitingTime;
        waitTimeProgressBar.value = waitTimePercentageRemaining;
        
        // Place the order
        if (!orderPlaced)
        {
            PlaceOrder();
        }

        // Checking if customer left the restaurant
        if (customer != null && Time.time - orderedTime > customer.WaitingTime)
        {
            UIManager.Instance.PrintText("Customer left the restaurant...");
           
            // If angry, double penalty
            if(angryIndex !=  -1)
            {
                ScoreManager.Instance[angryIndex] -= customer.GetPenalty() * 2;
            }
            else
            {
                ScoreManager.Instance[0] -= customer.GetPenalty();
                ScoreManager.Instance[1] -= customer.GetPenalty();
            }
            ResetToNewCustomer();
        }
    }

    void PlaceOrder()
    {
        orderedTime = Time.time;
        waitTimePercentageRemaining = (customer.WaitingTime - (Time.time - orderedTime)) / customer.WaitingTime;
        waitTimeProgressBar.value = waitTimePercentageRemaining;
        UpdateVegetableSprites();
        orderPlaced = true;
    }

    // Placing the order to customer
    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        // Check if order is correct
        if (vegetables != null && customer.IsOrderCorrect(vegetables))
        {
            UIManager.Instance.PrintText("Order completed.. Happy customer..");
            ScoreManager.Instance[_player.GetPlayerIndex()]+= 100;
            _player.IsHoldingSalad = false;
            for (int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i] = null;
            }
            vegetables.Clear();
            vegetables = null;
            ResetToNewCustomer();
            return true;
        }
        else
        {
            UIManager.Instance.PrintText("Wrong order.. Angry customer..");

            // Set customer as angry
            customer.SetAngry();
            angryIndex = _player.GetPlayerIndex();
            _player.IsHoldingSalad = true;
            base.PlaceIntoContainer(vegetables);
            
            // Return the order to player
            TransferVegetables(this, _player);
            return true;
        }
    }

    // Reset to new Customer
    private void ResetToNewCustomer()
    {
        customer = new Customer();
        angryIndex = -1;
        UpdateVegetableSprites();
        orderPlaced = false;
    }

    // Updating sprites
    protected override void UpdateVegetableSprites()
    {
        int i = 0;
        foreach (Vegetable vegetable in customer?.Order)
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
}
