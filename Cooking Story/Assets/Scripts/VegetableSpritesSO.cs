using System;
using UnityEngine;

[Serializable]
public struct VegetableSpriteSOEntry
{
    [SerializeField]
    public VegetableType vegetableType;
    [SerializeField]
    public Sprite sprite;
}

// Scriptable object to store the sprite for each vegetable
[CreateAssetMenu(fileName = "VegetableSpritesSO", menuName = "ScriptableObjects/VegetableSpritesSO", order = 1)]
public class VegetableSpritesSO : ScriptableObject
{
    [SerializeField]
    public VegetableSpriteSOEntry[] vegetables;
}
