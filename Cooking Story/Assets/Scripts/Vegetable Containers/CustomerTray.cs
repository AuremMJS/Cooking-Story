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
        customer = new Customer();
        SetMaxVegetables(100);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        waitTimePercentageRemaining = (customer.WaitingTime - (Time.time - orderedTime)) / customer.WaitingTime;
        waitTimeProgressBar.value = waitTimePercentageRemaining;
        if (!orderPlaced)
        {
            PlaceOrder();
        }
        if (customer != null && Time.time - orderedTime > customer.WaitingTime)
        {
            Debug.Log($"Customer left the restaurant {customer.WaitingTime}");
           
            if(angryIndex !=  -1)
            {
                ScoreManager.Instance[angryIndex] -= customer.GetPenalty() * 2;
            }
            else
            {
                ScoreManager.Instance[0] -= customer.GetPenalty();
                ScoreManager.Instance[1] -= customer.GetPenalty();
            }
            customer = new Customer();
            angryIndex = -1;
            UpdateVegetableSprites();
            orderPlaced = false;
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

    public override bool PlaceIntoContainer(List<Vegetable> vegetables)
    {
        if (vegetables != null && customer.IsOrderCorrect(vegetables))
        {
            Debug.Log("Order completed");
            ScoreManager.Instance[_player.GetPlayerIndex()]+= 100;
            _player.IsHoldingSalad = false;
            for (int i = 0; i < vegetables.Count; i++)
            {
                vegetables[i] = null;
            }
            vegetables.Clear();
            vegetables = null;
            customer = new Customer();
            angryIndex = -1;
            UpdateVegetableSprites();
            orderPlaced = false;
            return true;
        }
        else
        {
            Debug.Log("Order wrong");
            customer.SetAngry();
            angryIndex = _player.GetPlayerIndex();
            _player.IsHoldingSalad = true;
            base.PlaceIntoContainer(vegetables);
            TransferVegetables(this, _player);
            return true;
        }
    }

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
