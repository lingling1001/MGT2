using UnityEngine;

public abstract class BaseUI
{
    public Transform Trans { get; private set; }
    public GameObject ObjUI { get; private set; }
    public string UIPath { get; private set; }
    public object[] UIParams { get; private set; }
    public virtual EnumUIKind UIKind { get { return EnumUIKind.Normal; } }
    public void SetUIType(string type)
    {
        UIPath = type;
    }
    public void SetGameObject(GameObject obj, params object[] param)
    {
        Trans = obj.transform;
        ObjUI = obj;
        UIParams = param;
    }
    public virtual void OnInit()
    {

    }
    public virtual void OnRelease()
    {

    }
}

public enum EnumUIKind
{
    /// <summary>
    /// 常驻
    /// </summary>
    Alwary,
    /// <summary>
    /// 正常界面
    /// </summary>
    Normal,

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