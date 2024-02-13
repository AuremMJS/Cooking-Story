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
                return "Tomato";
            case VegetableType.Onion:
                return "Onion";
            case VegetableType.Lettuce:
                return "Lettuce";
            case VegetableType.Carrot:
                return "Carrot";
            case VegetableType.Jalapeno:
                return "Jalapeno";
            case VegetableType.Olives:
                return "Olives";
            default:
                return "";
        }
    }

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