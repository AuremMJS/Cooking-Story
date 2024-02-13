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

    void Awake()
    {
        Speed = CurrentSpeed = 10.0f;
    }
    public void ResetCurrentSpeed()
    {
        CurrentSpeed = Speed;
    }
}
