public class Vegetable
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