using com.ootii.Messages;
using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapEntityManager : Singleton<MapEntityManager>, IUpdate
{
    private Dictionary<int, AssemblyEntityBase> _mapEntities = new Dictionary<int, AssemblyEntityBase>();
    public Dictionary<int, AssemblyEntityBase> MapEntities { get { return _mapEntities; } }

    private List<AssemblyEntityBase> _listRemove = new List<AssemblyEntityBase>();
    private Transform _tranParent;
    public Transform TranParent { get { return _tranParent; } }
    public int Priority => DefinePriority.NORMAL;
    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit()
    {
        _tranParent = new GameObject("EntityManager").transform;
        RegisterInterfaceManager.RegisteUpdate(this);

    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        _listRemove.Clear();
        foreach (var item in MapEntities)
        {
            if (!item.Value.ContainsKey(EnumAssemblyType.EntityDead))
            {
                continue;
            }
            AssemblyEntityDead dead = item.Value.GetData<AssemblyEntityDead>(EnumAssemblyType.EntityDead);
            if (dead.IsFinish())
            {
                _listRemove.Add(item.Value);
            }
        }
    }

    /// <summary>
    /// 添加角色到地图中
    /// </summary>
    public void AddEntity(AssemblyEntityBase data)
    {
        if (data == null)
        {
            Log.Error("  is not Role Entity ");
            return;
        }
        if (MapEntities.ContainsKey(data.EntityId))
        {
            Log.Error(" MapEntities ContainsKey  "+ data.EntityId);
            return;
        }
        _mapEntities.Add(data.EntityId, data);
        AssyEntityManager.Instance.Addition(data.EntityId, data);
        MessageDispatcher.SendMessage(DefineNotification.MAP_ENTITY_ADD, data);
    }
    /// <summary>
    /// 是否包含角色
    /// </summary>
    public bool ContainsKey(int id)
    {
        return MapEntities.ContainsKey(id);
    }
    /// <summary>
    /// 获取实体
    /// </summary>
    public AssemblyEntityBase GetEntity(int id)
    {
        if (!MapEntities.ContainsKey(id))
        {
            return null;
        }
        return MapEntities[id];
    }
    /// <summary>
    /// 移除角色
    /// </summary>
    public void Remove(AssemblyEntityBase data)
    {
        if (data != null)
        {
            Remove(data.EntityId);
        }
    }
    /// <summary>
    /// 移除角色
    /// </summary>
    public void Remove(int id)
    {
        if (MapEntities.ContainsKey(id))
        {
            _mapEntities.Remove(id);
            MessageDispatcher.SendMessage(DefineNotification.MAP_ENTITY_REM, rData: id);
        }
        else
        {
            Log.Error("  id is error " + id);
        }
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        foreach (var item in MapEntities)
        {
            item.Value.OnRelease();
        }
        _mapEntities.Clear();
        if (TranParent != null)
        {
            GameObject.Destroy(_tranParent.gameObject);
            _tranParent = null;
        }
    }

}


public enum EnumEntityType : int
{
    /// <summary>
    /// 建筑。
    /// </summary>
    Building,
}