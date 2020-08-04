public class AssemblySelfRole : AssemblyBase
{
    public AssemblyRole SelfEntity
    {
        get
        {
            if (_selfEntity == null)
            {
                if (Owner.ContainsKey(EnumAssemblyType.Role))
                {
                    _selfEntity = Owner.GetData<AssemblyRole>(EnumAssemblyType.Role);
                }
            }
            return _selfEntity;
        }
    }
    private AssemblyRole _selfEntity;
}
