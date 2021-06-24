using com.ootii.Messages;
using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmGameStatePreLoad : FsmBase
{
    public FsmGameStatePreLoad(FsmManager fasManager, string strName) : base(fasManager, strName)
    {
    }
    public override void OnLoad()
    {
        MessageDispatcher.AddListener(NotificationName.RESOURCES_LOAD_FINISH, EventLoadFinish);
        PreResLoadHelper.Instance.OnInitPreLoad();
        base.OnLoad();
    }

    private void EventLoadFinish(IMessage rMessage)
    {
        PrototypeHelper.LoadAllData();
        ChangeState(FsmManagerGame.GAME_STATE_MAIN);

    }

    public override void OnRelease()
    {
        MessageDispatcher.RemoveListener(NotificationName.RESOURCES_LOAD_FINISH, EventLoadFinish);
        base.OnRelease();
    }


}
