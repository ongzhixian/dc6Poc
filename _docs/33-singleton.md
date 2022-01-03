# Singleton Patterns


```cs:simple thread-safe singleton
public sealed class Singleton
{
    private static Singleton instance = null;
    private static readonly object padlock = new object();

    Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
```

```cs:thread-safe singleton without locks (ie. static singletons)
public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    // See: https://csharpindepth.com/articles/singleton
    // See: https://csharpindepth.com/articles/BeforeFieldInit
    static Singleton()
    {
    }

    private Singleton()
    {
    }

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }
}
```

```cs:thread-safe lazy singletons
public sealed class Singleton
{
    private static readonly Lazy<Singleton> lazy =
        new Lazy<Singleton>(() => new Singleton());

    public static Singleton Instance { get { return lazy.Value; } }

    private Singleton()
    {
    }
}
```