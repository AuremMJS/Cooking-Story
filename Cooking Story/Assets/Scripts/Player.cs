using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float startTime;

    void Awake()
    {
        Speed = CurrentSpeed = 10.0f;
        GameTime = 60 * 2;
        startTime = Time.time;
    }

    void Update()
    {
        if(GameTime - (Time.time - startTime) <= 0)
        {
            CurrentSpeed = 0;
            Speed = 0;
        }
        else
        {
            UIManager.Instance.UpdateTimer(this);
        }
    }

    public void ResetCurrentSpeed()
    {
        CurrentSpeed = Speed;
    }
}
