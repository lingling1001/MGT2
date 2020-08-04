using MFrameWork;
using System.Collections.Generic;
using UnityEngine;

[MonoSingletonPath("PoolManager")]
public class GamePoolManager : MonoSingleton<GamePoolManager>, IUpdate
{
    private Dictionary<string, GamePoolData> _mapPool = new Dictionary<string, GamePoolData>();

    public int Priority => DefinePriority.NORMAL;

    protected override void OnInit()
    {
        RegisterInterfaceManager.RegisteUpdate(this);
        base.OnInit();
    }
    protected override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        base.OnRelease();
    }
    public Object GetPrefab(string prefabName)
    {
        if (_mapPool.ContainsKey(prefabName))
        {
            return _mapPool[prefabName].Prefab;
        }
        return null;
    }
    public bool Contain(string prefabName)
    {
        return _mapPool.ContainsKey(prefabName);
    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {


    }

}
public class GamePoolData
{
    public string PrefabName;
    public Object Prefab;
}