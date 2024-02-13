using System;

public class Vegetable : IEquatable<Vegetable>
{
    public VegetableType Type { get; set; }
    public string VegetableName {  get; private set; }
    public bool IsChopped {  get; set; }

    public Vegetable(VegetableType type, bool isChopped)
    {
        Type = type;
        VegetableName = GetVegetableName(type);
        IsChopped = isChopped;
    }

    private string GetVegetableName(VegetableType vegetableType)
    {
        switch (vegetableType)
        {
            case VegetableType.Tomato:
                return GameController.GameConstants.TOMATO_NAME;
            case VegetableType.Onion:
                return GameController.GameConstants.ONION_NAME;
            case VegetableType.Lettuce:
                return GameController.GameConstants.LETTUCE_NAME;
            case VegetableType.Carrot:
                return GameController.GameConstants.CARROT_NAME;
            case VegetableType.Jalapeno:
                return GameController.GameConstants.JALAPENO_NAME;
            case VegetableType.Olives:
                return GameController.GameConstants.OLIVES_NAME;
            default:
                return "";
        }
    }

    // Checking for equality
    public bool Equals(Vegetable other)
    {
        return Type == other.Type && IsChopped == other.IsChopped;
    }
}

public enum VegetableType
{
    Tomato,
    Onion,
    Lettuce,
    Carrot,
    Jalapeno,
    Olives
}