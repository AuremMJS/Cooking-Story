using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    public static SpriteLoader Instance;

    [SerializeField]
    private VegetableSpritesSO vegetableSpritesSO;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Sprite GetSpriteForVegetable(VegetableType type)
    {
        VegetableSpriteSOEntry vegetableSpriteSOEntry = vegetableSpritesSO.vegetables.FirstOrDefault<VegetableSpriteSOEntry>((veg) => { return veg.vegetableType == type; });
        return vegetableSpriteSOEntry.sprite;
    }
}
