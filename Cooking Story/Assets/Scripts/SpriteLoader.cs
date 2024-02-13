using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Loading the sprites
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

    // Find the correct sprite and load it
    public Sprite GetSpriteForVegetable(VegetableType type)
    {
        VegetableSpriteSOEntry vegetableSpriteSOEntry = vegetableSpritesSO.vegetables.FirstOrDefault<VegetableSpriteSOEntry>((veg) => { return veg.vegetableType == type; });
        return vegetableSpriteSOEntry.sprite;
    }
}
