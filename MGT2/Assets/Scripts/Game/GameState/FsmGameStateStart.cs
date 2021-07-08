using com.ootii.Messages;
using MFrameWork;
using UnityEngine;

public class FsmGameStateStart : FsmBase
{
    public FsmGameStateStart(FsmManager fasManager, string strName) : base(fasManager, strName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        GameManager<GameTimeManager>.QGetOrAddMgr();
        GameManager<CameraManager>.QGetOrAddMgr();
        GameManager<EntityManager>.QGetOrAddMgr();
        GameManager<WorldManager>.QGetOrAddMgr();
        GameManager<MapManager>.QGetOrAddMgr().SetMapData(1);

        UIManager.QOpenUI<UIHeadInfo>();
        UIManager.QOpenUI<UIMain>();
        UIManager.QOpenUI<UIJoystick>();
        UIManager.QOpenUI<UIRoleControl>();


    }
    public override void OnLeave()
    {
        GameManager<GameTimeManager>.QRemoveMgr();
        GameManager<MapManager>.QRemoveMgr();
        GameManager<CameraManager>.QRemoveMgr();
        GameManager<EntityManager>.QRemoveMgr();
        GameManager<WorldManager>.QRemoveMgr();

        UIManager.Instance.CloseAllUI(EnumUIKind.Normal);

        base.OnLeave();
    }


}
