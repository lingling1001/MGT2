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

        GameManager.QGetOrAddMgr<GameTimeManager>();
        GameManager.QGetOrAddMgr<CameraManager>();
        GameManager.QGetOrAddMgr<EntityManager>();
        GameManager.QGetOrAddMgr<WorldManager>();
        GameManager.QGetOrAddMgr<MapManager>().SetMapData(1);

        UIManager.QOpenUI<UIHeadInfo>();
        UIManager.QOpenUI<UIMain>();
        UIManager.QOpenUI<UIJoystick>();
        UIManager.QOpenUI<UIRoleControl>();


    }
    public override void OnLeave()
    {
        GameManager.QRemoveMgr<GameTimeManager>();
        GameManager.QRemoveMgr<MapManager>();
        GameManager.QRemoveMgr<CameraManager>();
        GameManager.QRemoveMgr<EntityManager>();
        GameManager.QRemoveMgr<WorldManager>();

        UIManager.Instance.CloseAllUI(EnumUIKind.Normal);

        base.OnLeave();
    }


}
