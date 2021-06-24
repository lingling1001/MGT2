using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResLoadManager : MonoSingleton<ResLoadManager>, IUpdate
{
    /// <summary>
    /// 正在加载列表
    /// </summary>
    private List<ResLoadData> _listLoadingAll = new List<ResLoadData>();
    private List<ResLoadData> _listLoadingCur = new List<ResLoadData>();
    private int _loadMaxCount = 2;
    private Dictionary<string, Object> _mapCacheAssets = new Dictionary<string, Object>();

    public int Priority => DefinePriority.RESLOAD;

    private void Awake()
    {
        RegisterInterfaceManager.RegisteUpdate(this);
    }
    private void OnDestroy()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);

    }
    public Object LoadAsset(string assetName)
    {
        return PreResLoadHelper.Instance.LoadAsset(assetName);
    }
    public T LoadAsset<T>(string assetName) where T : Object
    {
        Object loadObj = PreResLoadHelper.Instance.LoadAsset(assetName);
        if (loadObj != null)
        {
            T data = loadObj as T;
            if (data == null)
            {
                Log.Error("  LoadAsset Convert  Error " + assetName);
            }
            return data;
        }
        Log.Error("  LoadAsset   Error " + assetName);
        return null;
    }
    public void LoadAssetAsync(string assetName, System.Action<ResLoadData> eventLoadFinish)
    {
        ResLoadData load = _listLoadingAll.Find(item => item.ResName == assetName);
        if (load == null)
        {
            //Log.Info(" LoadAssetAsync {0}", assetName);
            load = ItemPoolMgr.CreateOrGetObj<ResLoadData>(ResLoadData.STR_KEY);
            load.SetData(assetName, EnumLoadState.None);
            _listLoadingAll.Add(load);
        }
        load.Callbacks.Add(eventLoadFinish);

    }
    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        while (_listLoadingCur.Count < _loadMaxCount && _listLoadingAll.Count > 0)
        {
            ResLoadData data = _listLoadingAll[0];
            _listLoadingCur.Add(data);//加入当前
            _listLoadingAll.Remove(data);//总数量移除
        }
        //Log.Info($"Load Count {_listLoadingCur.Count}");
        for (int cnt = 0; cnt < _listLoadingCur.Count; cnt++)
        {
            ResLoadData data = _listLoadingCur[cnt];
            if (data.State == EnumLoadState.None)
            {
                data.Start();
                continue;
            }
            if (data.IsDone())
            {
                data.Finish(EnumLoadState.Finish);
                _listLoadingCur.Remove(data);
                ItemPoolMgr.Instance.AddPoolItem(data);//加入缓存
            }
        }
    }



    public void LoadAssetInstantiateAsync(string assetName, System.Action<GameObject> eventLoadFinish)
    {
        Object asset = GetCacheAsset(assetName);
        if (asset == null)
        {
            LoadAssetAsync(assetName, (obj) =>
             {
                 if (obj.State == EnumLoadState.Finish)
                 {
                     Object resObj = obj.GetResult();
                     if (resObj == null)
                     {
                         Log.Error($"{obj.ToString()} Is Null ");
                         return;
                     }
                     AddCacheAsset(assetName, resObj);
                     eventLoadFinish(Object.Instantiate(resObj) as GameObject);
                 }
                 else
                 {
                     Log.Error($"{obj.ToString()}");
                 }

             });
        }
        else
        {
            eventLoadFinish(Instantiate(asset as GameObject));
        }
    }



    private void AddCacheAsset(string assetName, Object asset)
    {
        if (_mapCacheAssets.ContainsKey(assetName))
        {
            return;
        }
        _mapCacheAssets.Add(assetName, asset);
    }
    private Object GetCacheAsset(string assetName)
    {
        if (_mapCacheAssets.ContainsKey(assetName))
        {
            return _mapCacheAssets[assetName];
        }
        return null;
    }


}

