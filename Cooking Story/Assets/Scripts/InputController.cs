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
        if(Input.GetKeyDown(GameController.GameConstants.PLAYER1_TAKE))
        {
            ItemTaken?.Invoke(0);
        }
        else if (Input.GetKeyDown(GameController.GameConstants.PLAYER1_PLACE))
        {
            ItemPlaced?.Invoke(0);
        }

        if(Input.GetKeyDown(GameController.GameConstants.PLAYER2_TAKE)) 
        {
            ItemTaken?.Invoke(1);
        }
        else if(Input.GetKeyDown(GameController.GameConstants.PLAYER2_PLACE))
        {
            ItemPlaced?.Invoke(1);
        }
    }

    public Vector2 GetPlayerVelocity(int playerIndex)
    {
        string horizontalAxis = playerIndex == 0 ? GameController.GameConstants.PLAYER1_HORIZONTAL : GameController.GameConstants.PLAYER2_HORIZONTAL;
        string verticalAxis = playerIndex == 0 ? GameController.GameConstants.PLAYER1_VERTICAL : GameController.GameConstants.PLAYER2_VERTICAL;
        float xVelocity = Input.GetAxis(horizontalAxis);
        float yVelocity = Input.GetAxis(verticalAxis);
        return new Vector2(xVelocity, yVelocity);
    }
}
