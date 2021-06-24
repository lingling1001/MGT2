using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResLoadData : IMonoPool
{
    public const string STR_KEY = "ResLoadData";

    public string ResName { get; private set; }
    private AsyncOperationHandle<Object> _handle;
    public List<System.Action<ResLoadData>> Callbacks = new List<System.Action<ResLoadData>>();
    public EnumLoadState State { get { return _state; } }

    public string PoolKey { get; set; }


    private float _startTime;
    private EnumLoadState _state;

    public bool IsDone()
    {
        if (_handle.IsDone && _handle.Result != null)
        {
            return true;
        }
        if ((Time.time - _startTime) > 3)//超时
        {
            Log.Info(_handle.Status + "  " + _handle.Result);
            Finish(EnumLoadState.TimeOut);
            return true;
        }
        return false;
    }

    public Object GetResult()
    {
        return _handle.Result;
    }
    public void SetData(string strName, EnumLoadState state)
    {
        ResName = strName;
        SetState(state);
    }
    public void Start()
    {
        //Log.Info(" start Load {0}", ToString(),GetHashCode());
        SetState(EnumLoadState.Loading);
        _startTime = Time.time;
        _handle = Addressables.LoadAssetAsync<Object>(ResName);

    }
    public void Finish(EnumLoadState state)
    {
        SetState(state);
        for (int cnt = 0; cnt < Callbacks.Count; cnt++)
        {
            Callbacks[cnt](this);
        }
        //Log.Info($" Finish Load {ToString()}");
    }
    private void SetState(EnumLoadState state)
    {
        _state = state;
    }
    public void EnterPool()
    {
        SetState(EnumLoadState.None);
        Callbacks.Clear();
        ResName = string.Empty;
    }
    public override string ToString()
    {
        return $"Name : {ResName}   State : {State}";
    }
  
}
public enum EnumLoadState
{
    None,
    Loading,
    Finish,
    TimeOut,
}