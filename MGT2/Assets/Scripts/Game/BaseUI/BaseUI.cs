using UnityEngine;

public abstract class BaseUI
{
    private Transform _trans;
    public Transform Trans { get { return _trans; } }
    public GameObject ObjUI { get; private set; }
    public EnumUIType UIType { get; private set; }
    public void SetUIType(EnumUIType type)
    {
        UIType = type;
    }
    public void SetGameObject(GameObject obj)
    {
        ObjUI = obj;
        _trans = obj.transform;
    }
    public virtual void OnInit()
    {

    }
    public virtual void SetUIParam(params object[] param)
    {

    }
    public virtual void On_Update(float elapseSeconds, float realElapseSeconds)
    {

    }
    public virtual void OnRelease()
    {

    }
}

public static class ExtensionBaseUI
{
    public static T Find<T>(this BaseUI ui, string strPath) where T : Component
    {
        if (ui == null || ui.Trans == null)
        {
            Log.Error(" UI  Obj Is Null  " + ui);
            return null;
        }

        return ui.Trans.FindChild<T>(strPath);

    }

}