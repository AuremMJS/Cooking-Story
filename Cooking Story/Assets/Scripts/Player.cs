using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Common Player data class
public class Player : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }
    }
    public float Speed { get; set; }
    public float CurrentSpeed { get; set; }
    public int Score
    {
        get
        {
            return ScoreManager.Instance[playerIndex];
        }
    }

    public float GameTime { get; set; }

    void Start()
    {
        Speed = CurrentSpeed = GameController.GameConstants.DEFAULT_PLAYER_SPEED;
        GameTime = GameController.GameConstants.GAME_TIME;
    }

    // Reset speed after chopping
    public void ResetCurrentSpeed()
    {
        CurrentSpeed = Speed;
    }
}
