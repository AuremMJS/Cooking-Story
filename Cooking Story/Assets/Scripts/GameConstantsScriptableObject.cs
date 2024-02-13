using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstantsSO", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstantsScriptableObject : ScriptableObject
{
    public int DEFAULT_SCENE;
    public string PLAYER1_HORIZONTAL;
    public string PLAYER1_VERTICAL;
    public string PLAYER2_HORIZONTAL;
    public string PLAYER2_VERTICAL;
    public KeyCode PLAYER1_TAKE;
    public KeyCode PLAYER2_TAKE;
    public KeyCode PLAYER1_PLACE;
    public KeyCode PLAYER2_PLACE;
    public int GAME_TIME;
    public int DEFAULT_PLAYER_SPEED;
    public int MIN_ITEMS_IN_ORDER;
    public int MAX_ITEMS_IN_ORDER;
    public float TIME_FOR_ONE_ORDER;
    public float ANGRY_TIME_MULTIPLIER;
    public float NORMAL_TIME_MULTIPLIER;
    public int CUSTOMER_PENALTY;
    public int TRASH_PENALTY;
    public int MIN_ITEMS_IN_BASKET;
    public int MAX_ITEMS_IN_BASKET;
    public float CHOPPING_TIME;
    public int MAX_ITEMS_IN_PLAYER_TRAY;
    public int DEFAULT_NO_OF_ITEMS_TO_TAKE;
    public string TOMATO_NAME;
    public string ONION_NAME;
    public string LETTUCE_NAME;
    public string CARROT_NAME;
    public string JALAPENO_NAME;
    public string OLIVES_NAME;

}
