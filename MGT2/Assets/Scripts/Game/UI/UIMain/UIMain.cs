using com.ootii.Messages;
using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIMain : BaseUI, IUpdate
{
    public int Priority => DefinePriority.NORMAL;
    private GameTimeManager _gameTimeManager;

    public override void OnInit()
    {
        GetBindComponents(ObjUI);
        RegisterInterfaceManager.RegisteUpdate(this);
        EventHelper.AddListener(NotificationName.EventTimeState, EventTimeState);
        EventHelper.AddListener(NotificationName.EventGameSpeed, EventGameSpeed);

        EventHelper.RegistEvent(m_Btn_Main, EventClickMain);

        EventHelper.RegistEvent(m_Btn_Pause, EventClickPause);
        EventHelper.RegistEvent(m_Btn_Speed, EventClickSpeed);

        _gameTimeManager = GameManager<GameTimeManager>.QGetOrAddMgr();

        EventTimeState(null);
        EventGameSpeed(null);

    }



    private void EventTimeState(IMessage rMessage)
    {
        string strImage;
        if (_gameTimeManager.IsPause())
        {
            strImage = "btn_icon_player_pause";
        }
        else
        {
            strImage = "btn_icon_player_play";
        }
        UIHelper.SetAtlasImage(this.m_AImg_Pause, strImage);

    }
    private void EventGameSpeed(IMessage rMessage)
    {
        string strImage;
        if (_gameTimeManager.GameSpeed > 1)
        {
            strImage = "btn_icon_player_fw";
        }
        else
        {
            strImage = "btn_icon_player_play";
        }

        UIHelper.SetAtlasImage(this.m_Img_Speed, strImage);
        string strSpeed = UIHelper.GetStrCount(_gameTimeManager.GameSpeed, EnumCountStrType.X);
        UIHelper.SetText(this.m_Txt_Speed, strSpeed);

    }
    private void EventClickPause(Button btn)
    {
        _gameTimeManager.SetPauseState(!_gameTimeManager.IsPause());
    }

    private void EventClickPlay(Button btn)
    {
        _gameTimeManager.SetPauseState(true);
    }
    private void EventClickSpeed(Button btn)
    {
        _gameTimeManager.SetSpeed();


    }



    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        m_Txt_CurTime.text = _gameTimeManager.RunTime.ToString();
    }
    private void EventClickMain(Button btn)
    {
        GameManager<SaveEntityManager>.QGetOrAddMgr().SaveAllEntity();
        GameStateManager.Instance.ChangeState(FsmManagerGame.GAME_STATE_MAIN);
    }

    public override void OnRelease()
    {
        EventHelper.RemoveListener(NotificationName.EventTimeState, EventTimeState);
        EventHelper.RemoveListener(NotificationName.EventGameSpeed, EventGameSpeed);

        RegisterInterfaceManager.UnRegisteUpdate(this);
    }


    private void OnApplicationPause(bool focus)
    {
        if (focus)
        {
            Debug.Log("进入后台");
        }
        else
        {
            Debug.Log("进入前台");
        }
    }
}
