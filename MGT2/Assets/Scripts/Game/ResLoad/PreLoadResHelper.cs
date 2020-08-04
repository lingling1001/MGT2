using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFrameWork;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.ResourceManagement.ResourceLocations;
using com.ootii.Messages;

public class PreLoadResHelper : MonoSingleton<PreLoadResHelper>
{
    private List<string> _preLoads = new List<string>();
    /// <summary>
    /// 加载资源列表
    /// </summary>
    private List<IResourceLocation> _allDatas = new List<IResourceLocation>();
    private Dictionary<string, UnityEngine.Object> _mapPreLoadRes = new Dictionary<string, UnityEngine.Object>();
    /// <summary>
    /// 当前加载资源列表
    /// </summary>
    private List<IResourceLocation> _loadingList = new List<IResourceLocation>();
    private List<IResourceLocation> _loadFailed = new List<IResourceLocation>();
    private int _idxFailed = 0;
    private int _loadingCount = 50;
    public UnityEngine.Object LoadAsset(string assetName)
    {
        if (_mapPreLoadRes.ContainsKey(assetName))
        {
            return _mapPreLoadRes[assetName];
        }
        return null;
    }

    public IResourceLocation GetResourceLocation(string assetName)
    {
        return _allDatas.Find(item => item.PrimaryKey == assetName); ;
    }
    public void OnInitPreLoad()
    {
        ResLoadHelper.LoadResourceLocationsAsync("preLoad", EventLoadFinishLocation);
    }
    private void EventLoadFinishLocation(List<IResourceLocation> objs)
    {
        _preLoads.Clear();
        _allDatas.Clear();
        for (int cnt = 0; cnt < objs.Count; cnt++)
        {
            _preLoads.Add(objs[cnt].PrimaryKey);
            _allDatas.Add(objs[cnt]);
        }
        StartCoroutine(StartLoad());
    }
    private WaitForEndOfFrame _wait = new WaitForEndOfFrame();
    private IEnumerator StartLoad()
    {
        if (_preLoads.Count > 0)
        {
            int idx = 0;
            while (idx < _preLoads.Count && _loadingList.Count < _loadingCount)
            {
                _loadingList.Add(GetResourceLocation(_preLoads[idx]));
                idx++;
            }
            ResLoadHelper.LoadAssetsAsync<UnityEngine.Object>(_loadingList, EventLoadFinishRes);
        }
        yield return _wait;
    }

    private void EventLoadFinishRes(UnityEngine.Object obj)
    {
        IResourceLocation curRes = null;
        for (int cnt = 0; cnt < _loadingList.Count; cnt++)
        {
            IResourceLocation item = _loadingList[cnt];
            //名称包含并且资源类型是一类
            if (item.PrimaryKey.Contains(obj.name) && obj.GetType() == item.ResourceType)
            {
                curRes = item;
                break;
            }
        }
        if (curRes == null)
        {
            _idxFailed++;
        }
        else
        {
            //Log.Info("  Load Res " + curRes.PrimaryKey);
            _preLoads.Remove(curRes.PrimaryKey);
            AddRes(curRes.PrimaryKey, obj);
            _loadingList.Remove(curRes);
        }
        if (_loadingList.Count == _idxFailed)//失败数量
        {
            if (_loadingList.Count != 0)
            {
                _loadFailed.AddRange(_loadingList);
                _loadingList.Clear();
                _idxFailed = 0;
            }
            //开启下一轮加载， 未考虑资源分类
            StartCoroutine(StartLoad());
            if (_preLoads.Count == 0)
            {
                if (_loadFailed.Count > 0)
                {
                    Log.Error(" ---------资源加载失败 " + _loadFailed.Count + " ----------");
                    for (int cnt = 0; cnt < _loadFailed.Count; cnt++)
                    {
                        Log.Error(cnt + "   " + _loadFailed[cnt].PrimaryKey);
                    }
                    Log.Error("-----------------华丽的分割线-----------------");
                }
                Log.Info(" 加载 资源 完成 资源数量 ： " + _mapPreLoadRes.Count);
                MessageDispatcher.SendMessage(DefineNotification.RESOURCES_LOAD_FINISH);
            }
        }

    }
    private void AddRes(string strKey, UnityEngine.Object res)
    {
        if (_mapPreLoadRes.ContainsKey(strKey))
        {
            Log.Error("  Add Res Key Error " + strKey);
            return;
        }
        _mapPreLoadRes.Add(strKey, res);
    }

    private void OnDestroy()
    {
        _mapPreLoadRes.Clear();
    }
}
