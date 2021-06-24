using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAssembly : ISubjectAssembly
{

    private List<AssemblyBase> _listDatas = new List<AssemblyBase>();
    private List<IObserverAssembly> _observers = new List<IObserverAssembly>();
    public int EntityId;
    public int EntityType;

    [Newtonsoft.Json.JsonIgnore]
    public List<AssemblyBase> ListDatas { get { return _listDatas; } }

    public void SetEntityId(int entityId, int type)
    {
        EntityId = entityId;
        EntityType = type;
    }
    public virtual void AddData(AssemblyBase data)
    {
        bool isCanAdd = EntityHelper.AssemblyIsMultipe(data);//多个组件
        if (!isCanAdd)
        {
            isCanAdd = !ContainsKey(data.GetType());//重复组件
        }
        if (!isCanAdd)
        {
            Log.Error(" cant add data key repeat {0}  {1}", data.GetType(), data);
            return;
        }
        _listDatas.Add(data);
        NotifyObserver(EnumAssemblyOperate.Addition, data);
    }

    public T GetData<T>() where T : AssemblyBase, new()
    {
        AssemblyBase bData = GetData(typeof(T));
        if (bData != null)
        {
            return bData as T;
        }
        return null;
    }
    public AssemblyBase GetData(System.Type type)
    {
        return ListDatas.Find(item => item.GetType() == type);
    }
    public List<T> GetDatas<T>(System.Type type) where T : AssemblyBase, new()
    {
        List<T> listRes = new List<T>();
        for (int cnt = 0; cnt < ListDatas.Count; cnt++)
        {
            if (ListDatas[cnt].GetType() == type)
            {
                listRes.Add(ListDatas[cnt] as T);
            }
        }
        return listRes;
    }

    public void Remove(System.Type type)
    {
        AssemblyBase data = GetData(type);
        if (data != null)
        {
            _listDatas.Remove(data);
        }
    }

    public bool ContainsKey<T>() where T : AssemblyBase, new()
    {
        return GetData(typeof(T)) != null;
    }

    public bool ContainsKey(System.Type type)
    {
        return GetData(type) != null;
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
        for (int cnt = 0; cnt < ListDatas.Count; cnt++)
        {
            ListDatas[cnt].Release();
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
    /// <summary>
    /// 行为变更
    /// </summary>
    ActionChange,
}