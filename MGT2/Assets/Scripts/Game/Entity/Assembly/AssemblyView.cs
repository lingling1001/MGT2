
using System;
using UnityEngine;
public class AssemblyView : AssemblyBase
{
    public string EntityPath;
    public string EntityLayer;
    [Newtonsoft.Json.JsonIgnore] public Transform Trans { get; private set; }
    [Newtonsoft.Json.JsonIgnore] public GameObject ObjEntity { get; private set; }

    public void SetPath(string strPath)
    {
        SetPath(strPath, DefineLayer.ENTITY);
    }
    public void SetPath(string strPath, string layer)
    {
        EntityPath = strPath;
        EntityLayer = layer;
        ReadDataFinish();
    }
    public override void ReadDataFinish()
    {
        if (string.IsNullOrEmpty(EntityPath))
        {
            return;
        }
        ResLoadManager.Instance.LoadAssetInstantiateAsync(EntityPath, EventLoadFinish);
    }


    private void EventLoadFinish(GameObject obj)
    {
        if (Owner == null)
        {
            GameObject.DestroyImmediate(obj);
            Log.Error(EntityPath + "   EventLoadFinish  Entity Is Null   ");
            return;
        }
        ObjEntity = obj;
        Trans = obj.transform;
        Owner.NotifyObserver(EnumAssemblyOperate.ViewLoadFinish, this);

    }

    public bool ObjEntityIsNull()
    {
        if (ObjEntity == null || ObjEntity.Equals(null))
        {
            return true;
        }
        return false;
    }

    protected override void OnRelease()
    {
        if (!ObjEntityIsNull())
        {
            GameObject.Destroy(ObjEntity);
            ObjEntity = null;
        }
        base.OnRelease();
    }



}
