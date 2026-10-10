namespace pr1.Base;

public static class Validation
{
    public static bool IsEmpty(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }
        
        return false;
    }

    public static bool IsPositivePrice(decimal value)
    {
        if (value > 0)
        {
            return true;
        }
        
        return false;
    }
}