﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int[] player_Score;
    public int this[int index]
    {
        get {
            if (player_Score == null || player_Score.Length < 2)
            {
                player_Score = new int[2];
            }
            return player_Score[index];
        }
        set {
            Debug.Log($"Score reduced {player_Score[index]} -> {value}");
            player_Score[index] = value > 0 ? value : 0;
            ScoreUpdated?.Invoke();
        }
    }

    public Action ScoreUpdated;
    
    public static ScoreManager Instance;
    public void Awake()
    {
        player_Score = new int[2];
        if (Instance == null)
            Instance = this;
    }
}
