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
        MessageDispatcher.AddListener(DefineNotification.RESOURCES_LOAD_FINISH, EventLoadFinish);
        PreLoadResHelper.Instance.OnInitPreLoad();
        base.OnLoad();
    }

    private void EventLoadFinish(IMessage rMessage)
    {
        PrototypeHelper.LoadAllData();
        UIManager.Instance.OnInit();

        ChangeState(FsmManagerGame.GAME_STATE_MAIN);

    }

    public override void OnRelease()
    {
        UIManager.Instance.OnRelease();
        MessageDispatcher.RemoveListener(DefineNotification.RESOURCES_LOAD_FINISH, EventLoadFinish);
        base.OnRelease();
    }


}
