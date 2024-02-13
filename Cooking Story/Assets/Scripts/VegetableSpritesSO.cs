using System;
using UnityEngine;

[Serializable]
public struct VegetableSpriteSOEntry
{
    [SerializeField]
    VegetableType vegetableType;
    [SerializeField]
    Sprite sprite;
}

[CreateAssetMenu(fileName = "VegetableSpritesSO", menuName = "ScriptableObjects/VegetableSpritesSO", order = 1)]
public class VegetableSpritesSO : ScriptableObject
{
    [SerializeField]
    private VegetableSpriteSOEntry[] vegetables;
}
