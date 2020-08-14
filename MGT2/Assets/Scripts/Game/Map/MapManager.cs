using com.ootii.Messages;
using MFrameWork;
using System;
using UnityEngine;
public class MapManager : Singleton<MapManager>
{
    public GameObject ObjTerrain;
    private PrototypeMap _mapData;
    public PrototypeMap MapData { get { return _mapData; } }

    public int Priority => throw new NotImplementedException();

    public void OnInit()
    {
        UIManager.Instance.OpenUI<UILoading>(EnumUIType.UILoading);
        LoadMap(1);

    }
    public void LoadMap(int mapId)
    {
        PrototypeMap data = PrototypeManager<PrototypeMap>.Instance.GetPrototype(mapId);
        SetMapData(data);
        ResLoadHelper.LoadAssetAsync<GameObject>(data.Path, EventLoadMapFinish);

    }

    private void EventLoadMapFinish(GameObject obj)
    {
        ObjTerrain = ResLoadHelper.Instantiate<GameObject>(obj);
        ObjTerrain.transform.position = Vector3.zero;
        ObjTerrain.transform.eulerAngles = Vector3.zero;

        MessageDispatcher.SendMessage(DefineNotification.MAP_LOAD_FINISH, 1f);

    }

    private void SetMapData(PrototypeMap data)
    {
        _mapData = data;
        FindPathManager.Instance.RefreshMapInfo(data);
    }




    public void OnRelease()
    {
        CameraManager.Release();
        GameObject.Destroy(ObjTerrain);
    }


}
