public abstract class AssemblyGetViewBase : AssemblyBase, IObserverAssembly
{
    protected AssemblyView assemblyView;

    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        Owner.RegisterObserver(this);
        if (owner.ContainsKey(EnumAssemblyType.View))
        {
            if (!GetView().ObjEntityIsNull())
            {
                ViewLoadFinish();
            }
        }
    }

    public AssemblyView GetView()
    {
        if (assemblyView == null)
        {
            assemblyView = Owner.GetData<AssemblyView>(EnumAssemblyType.View);
        }
        return assemblyView;
    }
    protected bool ViewObjIsNull()
    {
        if (assemblyView == null || assemblyView.ObjEntityIsNull())
        {
            return true;
        }
        return false;
    }
    public override void OnRelease()
    {
        Owner.RemoveObserver(this);
        base.OnRelease();
    }

    public virtual void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (data.AssemblyType != EnumAssemblyType.View)
        {
            return;
        }
        if (operate == EnumAssemblyOperate.ViewLoadFinish)
        {
            ViewLoadFinish();
        }
        else if (operate == EnumAssemblyOperate.Addition)
        {
            GetView();
        }
    }

    public abstract void ViewLoadFinish();

}