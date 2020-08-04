
using System;
using UnityEngine;
public class AssemblyView : AssemblyBase
{
    public string EntityName { get; private set; }
    public string EntityPath { get; private set; }
    public string EntityLayer { get; private set; }
    public GameObject ObjEntity { get; private set; }
    public Transform Trans { get; private set; }

    public void SetPath(string strName, string param, string layer)
    {
        EntityPath = param;
        EntityLayer = layer;
        EntityName = strName;
        ResLoadHelper.LoadAssetInstantiateAsync(EntityPath, EventLoadFinish, MapEntityManager.Instance.TranParent);
        Log.Info(strName + "  " + param);

    }

    private void EventLoadFinish(GameObject obj)
    {
        if (Owner == null)
        {
            GameObject.DestroyImmediate(obj);
            Log.Error(EntityPath + "   EventLoadFinish  Entity Is Null   " + EntityName);
            return;
        }
        ObjEntity = obj;
        ObjEntity.name = EntityName;
        Trans = obj.transform;
        ObjEntity.layer = LayerMask.NameToLayer(EntityLayer);
        LinkMonoView link = ObjEntity.GetOrAddComponent<LinkMonoView>();
        link.Link(this.Owner as AssemblyEntityBase);
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

    public override void OnRelease()
    {
        if (!ObjEntityIsNull())
        {
            GameObject.Destroy(ObjEntity);
            ObjEntity = null;
        }
        base.OnRelease();
    }



}
