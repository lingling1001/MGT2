using MFrameWork;
using System;
using System.Collections.Generic;

public class AssyEntityManager : Singleton<AssyEntityManager>
{
    private DataModesEntity _datas = null;
    private MapEntityKeyHelper _keyHelper = null;
    public void OnInit()
    {
        _datas = new DataModesEntity();
        _keyHelper = new MapEntityKeyHelper();
    }
    public bool ContainEntity(int id)
    {
        return _datas.ContainsKey(id);
    }

    public void Addition(int id, AssemblyEntityBase entity)
    {
        if (ContainEntity(id))
        {
            Log.Error(" Addition  Key  Error " + id);
            return;
        }
        _datas.Addition(id, entity);
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
        AssemblyEntityBase data = _datas.GetData(key);
        _keyHelper.RemoveKey(key);
        _datas.Remove(key);
        FactoryEntity.ReleaseEnitity(data);
    }

    public void OnRelease()
    {
        List<AssemblyEntityBase> list = _datas.GetListDatas();
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            FactoryEntity.ReleaseEnitity(list[cnt]);
        }
        _datas.Clear();
        _keyHelper.Release();
    }
}
