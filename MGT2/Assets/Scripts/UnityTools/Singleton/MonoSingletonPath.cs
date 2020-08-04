using System;

[AttributeUsage(AttributeTargets.Class)]
public class MonoSingletonPath : Attribute
{
    private string mPathInHierarchy;

    public MonoSingletonPath(string pathInHierarchy)
    {
        mPathInHierarchy = pathInHierarchy;
    }

    public string PathInHierarchy
    {
        get { return mPathInHierarchy; }
    }
}

