namespace pr1.Base;

public static class IdGenerator
{
    private static int _current = 0;

    public static int NextId()
    {
        return ++_current;
    }
}