using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyEntityBase : ISubjectAssembly
{
    public int EntityId { get; private set; }
    public List<AssemblyBase> ListDatas { get { return _listDatas; } }
    private List<AssemblyBase> _listDatas = new List<AssemblyBase>();
    private List<IObserverAssembly> _observers = new List<IObserverAssembly>();
    public bool IsRelease = false;
    public void SetEntityId(int entityId)
    {
        EntityId = entityId;
    }
    public virtual void AddData(AssemblyBase data)
    {
        bool isCanAdd = EntityHelper.AssemblyIsMultipe(data.AssemblyType);//多个组件
        if (!isCanAdd)
        {
            isCanAdd = !ContainsKey(data.AssemblyType);//重复组件
        }
        if (!isCanAdd)
        {
            Log.Error(" cant add data " + data.AssemblyType + "  " + data.ToString());
            return;
        }
        _listDatas.Add(data);
        NotifyObserver(EnumAssemblyOperate.Addition, data);
    }
    public AssemblyBase GetData(EnumAssemblyType type)
    {
        return ListDatas.Find(item => item.AssemblyType == type);
    }

    public T GetData<T>(EnumAssemblyType type) where T : AssemblyBase, new()
    {
        return GetData(type) as T;
    }

    public List<T> GetDatas<T>(EnumAssemblyType type) where T : AssemblyBase, new()
    {
        List<T> listRes = new List<T>();
        for (int cnt = 0; cnt < ListDatas.Count; cnt++)
        {
            if (ListDatas[cnt].AssemblyType == type)
            {
                listRes.Add(ListDatas[cnt] as T);
            }
        }
        return listRes;
    }

    public void Remove(EnumAssemblyType type)
    {
        AssemblyBase data = GetData(type);
        if (data != null)
        {
            _listDatas.Remove(data);
        }
    }
    public bool ContainsKey(EnumAssemblyType key)
    {
        return GetData(key) != null;
    }
    public bool ContainsKey(params EnumAssemblyType[] keys)
    {
        for (int cnt = 0; cnt < keys.Length; cnt++)
        {
            if (!ContainsKey(keys[cnt]))
            {
                return false;
            }
        }
        return true;
    }

    public void NotifyObserver(EnumAssemblyOperate operate, IAssembly data)
    {
        for (int cnt = 0; cnt < _observers.Count; cnt++)
        {
            _observers[cnt].UpdateAssembly(operate, data);
        }
    }

    public void RegisterObserver(IObserverAssembly observer)
    {
        if (_observers.Contains(observer))
        {
            return;
        }
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserverAssembly observer)
    {
        _observers.Remove(observer);
    }

    public virtual void OnRelease()
    {
        _observers.Clear();
        IsRelease = true;

        for (int cnt = 0; cnt < ListDatas.Count; cnt++)
        {
            ListDatas[cnt].OnRelease();
        }
        _listDatas.Clear();
    }


}


public interface IObserverAssembly
{
    void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data);
}

public interface ISubjectAssembly
{
    void RegisterObserver(IObserverAssembly observer);
    void RemoveObserver(IObserverAssembly observer);
    void NotifyObserver(EnumAssemblyOperate operate, IAssembly data);
}

public enum EnumAssemblyOperate
{
    Addition,
    Remove,
    ViewLoadFinish,
    Position,
    Attribute,
    /// <summary>
    /// 头顶信息点查找完成
    /// </summary>
    TransHead,
    /// <summary>
    /// 移动状态变更
    /// </summary>
    RoleMoveState,
    /// <summary>
    /// 摇杆移动
    /// </summary>
    JoystickMove,
    /// <summary>
    /// 技能状态变更 
    /// </summary>
    AbilityState,
}