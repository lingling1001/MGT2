using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : ManagerBase
{
    public bool IsLoadFinish { get; private set; }
    public PrototypeMap MapData { get { return _mapData; } }
    private PrototypeMap _mapData;
    private GameObject _objTerrain;
    private ASMakeManager _makeManager;
    private FindPathTools _findPath;
    public FindPathTools FindPath { get { return _findPath; } }
    public override void On_Init()
    {

        _findPath = new FindPathTools();
    }
    public void SetMapData(int id)
    {
        IsLoadFinish = false;
        PrototypeMap map = PrototypeManager<PrototypeMap>.Instance.GetPrototype(id);
        SetMapData(map);

    }
    public void SetMapData(PrototypeMap data)
    {
        _mapData = data;
        ResLoadManager.Instance.LoadAssetInstantiateAsync(data.Path, EventLoadTerrainFinish);

    }

    private void EventLoadTerrainFinish(GameObject obj)
    {
        NGUITools.ResetTransform(obj.transform);
        _objTerrain = obj;
        if (obj == null)
        {
            return;
        }
        _makeManager = obj.GetComponentInChildren<ASMakeManager>();
        if (_makeManager == null)
        {
            Log.Error("未获取地图数据 MapId ", MapData.PrototypeId);
            return;
        }
        string strMapStarName = PrototypeHelper.GetConfigName(_makeManager.MapStarName, string.Empty);
        _findPath.InitialMapInfo(strMapStarName, _makeManager.MapSize, _makeManager.NodeSize);
        IsLoadFinish = true;

        MessageDispatcher.SendMessage(NotificationName.EventMapLoadFinish, 0.1f);

    }

    public override void On_Release()
    {
        if (_findPath != null)
        {
            _findPath.OnRelease();
            _findPath = null;
        }
        ResLoadHelper.DestroyObject(_objTerrain);
    }

}
