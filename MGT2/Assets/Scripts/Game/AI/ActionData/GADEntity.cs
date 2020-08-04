public class GADEntity : GADataBase
{
    public AssemblyRole Entity { get; private set; }
    public void SetEntity(AssemblyRole entity)
    {
        Entity = entity;
    }
}
