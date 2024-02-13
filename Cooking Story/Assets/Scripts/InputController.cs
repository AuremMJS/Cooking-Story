using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action<int> ItemTaken { get; set; }
    public Action<int> ItemPlaced { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ItemTaken?.Invoke(0);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ItemPlaced?.Invoke(0);
        }

        if(Input.GetKeyDown(KeyCode.K)) 
        {
            ItemTaken?.Invoke(1);
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            ItemPlaced?.Invoke(1);
        }
    }

    public Vector2 GetPlayerVelocity(int playerIndex)
    {
        string horizontalAxis = playerIndex == 0 ? "P1_Horizontal" : "P2_Horizontal";
        string verticalAxis = playerIndex == 0 ? "P1_Vertical" : "P2_Vertical";
        float xVelocity = Input.GetAxis(horizontalAxis);
        float yVelocity = Input.GetAxis(verticalAxis);
        return new Vector2(xVelocity, yVelocity);
    }
}
