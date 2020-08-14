using com.ootii.Messages;
using MFrameWork;
public class FsmGameStateStart : FsmBase
{
    public FsmGameStateStart(FsmManager fasManager, string strName) : base(fasManager, strName)
    {
    }

    public override void OnEnter()
    {
        UIManager.Instance.OpenUI<UIMain>(EnumUIType.UIMain);

        MapManager.Instance.OnInit();
        MessageDispatcher.AddListener(DefineNotification.MAP_LOAD_FINISH, EventMapLoadFinish);

    }
    private void EventMapLoadFinish(IMessage rMessage)
    {

        MapEntityManager.Instance.OnInit();

        MonsterManager.Instance.OnInit();

        CameraManager.Initial();
        RoleManager.Instance.OnInit();

        RoleManager.Instance.InitRoleToMap();

        //UIManager.Instance.OpenUI<UIMinMap>(EnumUIType.UIMinMap);//打开小地图
        UIManager.Instance.OpenUI<UIPlaceRole>(EnumUIType.UIPlaceRole);
        UIManager.Instance.OpenUI<UIRoleControl>(EnumUIType.UIRoleControl);
        UIManager.Instance.CloseUI(EnumUIType.UILoading);


    }
    public override void OnLeave()
    {
        UIManager.Instance.CloseUI(EnumUIType.UIPlaceRole);
        UIManager.Instance.CloseUI(EnumUIType.UIMain);

        UIManager.Instance.CloseUI(EnumUIType.UIRoleControl);

        RoleManager.Instance.OnRelease();
        MonsterManager.Instance.OnRelease();

        MapEntityManager.Instance.OnRelease();

        MapManager.Instance.OnRelease();
        FindPathManager.Instance.OnRelease();

        MessageDispatcher.RemoveListener(DefineNotification.MAP_LOAD_FINISH, EventMapLoadFinish);

        base.OnLeave();

    }
}
