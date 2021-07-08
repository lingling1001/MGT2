using com.ootii.Messages;
using MFrameWork;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class WorldManager : ManagerBase
{
    public override void On_Init()
    {
        EventHelper.AddListener(NotificationName.EventMapLoadFinish, EventMapLoadFinish);

    }
    public EnumEnterType EnterType { get; private set; }
    public void SetEnterType(EnumEnterType type)
    {
        EnterType = type;
    }

    private void EventMapLoadFinish(IMessage rMessage)
    {
        if (EnterType == EnumEnterType.Load)
        {
            GameManager<SaveEntityManager>.QGetOrAddMgr().LoadAllEntity();
        }
        else
        {
            WorldCreateHelper.CreateEntities();
        }
        EventHelper.SendMessage(NotificationName.EventEntityInitial);
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    private void GenerateGameEntity()
    {






    }








    public override void On_Release()
    {
        EventHelper.RemoveListener(NotificationName.EventMapLoadFinish, EventMapLoadFinish);
        base.On_Release();
    }







}
public enum EnumEnterType
{
    Load,
    NewGame,
}
