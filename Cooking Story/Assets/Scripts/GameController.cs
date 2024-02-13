using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameConstantsScriptableObject gameConstants;
    [SerializeField]
    private Player[] players;

    // Singleton to constants SO
    public static GameConstantsScriptableObject GameConstants;

    private float startTime;
    private bool gameOver;

    void Awake()
    {
        startTime = Time.time;
        gameOver = false;
        GameConstants = gameConstants;
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for time out and game over
        int playersTimedOut = 0;
        foreach (Player player in players)
        {
            if (player.GameTime - (Time.time - startTime) <= 0)
            {
                player.CurrentSpeed = 0;
                player.Speed = 0;
                playersTimedOut++;
            }
            else
            {
                UIManager.Instance.UpdateTimer(player);
            }
        }
        if (playersTimedOut == players.Length && !gameOver)
        {
            // Compare player scores
            int player1Score = players[0].Score;
            int player2Score = players[1].Score;
            if (player1Score == player2Score)
            {
                UIManager.Instance.PrintText($"Game over! Game draw!");
            }
            else
            {
                int highScorePlayerIndex = player1Score > player2Score ? 1 : 2;
                UIManager.Instance.PrintText($"Game over! Player {highScorePlayerIndex} won the game!");
            }
            UIManager.Instance.ShowResetButton();
            gameOver = true;
        }
    }
}
