using UnityEngine;

public abstract class AssemblyGetViewBase : AssemblyBase, IObserverAssembly
{
    protected AssemblyView assemblyView;

    protected override void OnInit(EntityAssembly owner)
    {
        base.OnInit(owner);
        Owner.RegisterObserver(this);
        if (owner.ContainsKey<AssemblyView>())
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
            assemblyView = Owner.GetData<AssemblyView>();
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
    protected override void OnRelease()
    {
        Owner.RemoveObserver(this);
        base.OnRelease();
    }

    public virtual void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (!(data is AssemblyView))
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

    public virtual void ViewLoadFinish()
    {
        if (ViewObjIsNull())
        {
            return;
        }
        Transform transParent = TransparentNode.GetTransParent(EnumTransParent.Entities);
        if (transParent != null)
        {
            GetView().Trans.SetParent(transParent);
        }
        LinkMonoView link = GetView().ObjEntity.AddMissingComponent<LinkMonoView>();
        link.Link(this.Owner);

    }

}