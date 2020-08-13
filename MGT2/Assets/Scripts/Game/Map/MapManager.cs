using com.ootii.Messages;
using MFrameWork;
using System;
using UnityEngine;

public class MapManager : Singleton<MapManager>, IUpdate
{
    public GameObject ObjTerrain;
    private PrototypeMap _mapData;
    public PrototypeMap MapData { get { return _mapData; } }

    public int Priority => throw new NotImplementedException();

    private AStarThread _astartThread;
    public void OnInit()
    {
        _astartThread = new AStarThread(1, 1000);
        TaskAsynManager.Instance.AdditionTask(_astartThread);
        RegisterInterfaceManager.RegisteUpdate(this);
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
    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (_astartThread != null)
        {
            Log.Info(_astartThread.Ticks + "  " + _astartThread.Frame);
        }

    }
    public void OnRelease()
    {
        TaskAsynManager.Instance.FinishTask(1);
        RegisterInterfaceManager.UnRegisteUpdate(this);
        CameraManager.Release();
        GameObject.Destroy(ObjTerrain);
    }


}
