

public class AssemblyEntity : AssemblyBase
{
    public int EntityId { get; private set; }
    public int EntityType { get; private set; }

    public void SetEntityId(int param, int type)
    {
        EntityId = param;
        EntityType = type;
    }
}
