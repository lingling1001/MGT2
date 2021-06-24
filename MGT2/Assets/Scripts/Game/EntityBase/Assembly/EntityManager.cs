using com.ootii.Messages;
using MFrameWork;
using System;
using System.Collections.Generic;

public class EntityManager : ManagerBase
{
    private DataModesEntity _datas = null;
    private EntityKeyHelper _keyHelper = null;
    public override void On_Init()
    {
        _datas = new DataModesEntity();
        _keyHelper = new EntityKeyHelper();
    }
    public List<EntityAssembly> GetListDatas()
    {
        return _datas.GetListDatas();
    }
    public Dictionary<int, EntityAssembly> GetAllDatas()
    {
        return _datas.AllDatas;
    }

    public List<T> GetListDatas<T>() where T : AssemblyBase, new()
    {
        List<T> list = new List<T>();
        Dictionary<int, EntityAssembly> map = GetAllDatas();
        foreach (var item in map)
        {
            T target = item.Value.GetData<T>();
            if (target != null)
            {
                list.Add(target);
            }
        }
        return list;
    }


    public bool ContainEntity(int id)
    {
        return _datas.ContainsKey(id);
    }
    public EntityAssembly GetEntity(int id)
    {
        return _datas.GetData(id);
    }

    public bool Addition(EntityAssembly entity)
    {
        if (ContainEntity(entity.EntityId))
        {
            Log.Error(" Addition  Key  Error " + entity.EntityId);
            return false;
        }
        _datas.Addition(entity.EntityId, entity);
        return true;
    }
    /// <summary>
    /// 添加Key
    /// </summary>
    public void AdditionKey(EntityAssembly entity)
    {
        if (Addition(entity))
        {
            _keyHelper.AddKey(entity.EntityId);
        }

    }
    /// <summary>
    /// 获取空余的Key
    /// </summary>
    public int GetFreeEntityKey()
    {
        return _keyHelper.GetKey();
    }
    public void Remove(int key)
    {
        if (!ContainEntity(key))
        {
            Log.Error(" Remove Key  Error " + key);
            return;
        }
        EntityAssembly data = _datas.GetData(key);
        data.OnRelease();
        _keyHelper.RemoveKey(key);
        _datas.Remove(key);
    }

    public override void On_Release()
    {
        List<EntityAssembly> list = _datas.GetListDatas();
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            list[cnt].OnRelease();
        }
        _datas.Clear();
        _keyHelper.Release();
    }
}
