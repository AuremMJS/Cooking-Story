﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private Text player1Score;
    [SerializeField]
    private Text player2Score;
    [SerializeField]
    private Text player1Timer;
    [SerializeField]
    private Text player2Timer;

    private float startTime;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        ScoreManager.Instance.ScoreUpdated += UpdateScores;
        UpdateScores();
        startTime = Time.time;
    }

    public void UpdateScores()
    {
        player1Score.text = ScoreManager.Instance[0].ToString();
        player2Score.text = ScoreManager.Instance[1].ToString();
    }

    public void UpdateTimer(Player player)
    {
        int timeLeftInSeconds = (int)(player.GameTime - (Time.time - startTime));
        string timeText = string.Format($"{timeLeftInSeconds / 60}:{timeLeftInSeconds % 60}");
        if (player.PlayerIndex == 0)
            player1Timer.text = timeText;
        else if (player.PlayerIndex == 1)
            player2Timer.text = timeText;
    }
}
