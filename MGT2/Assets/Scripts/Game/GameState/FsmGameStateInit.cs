using MFrameWork;

public class FsmGameStateInit : FsmBase
{
    public FsmGameStateInit(FsmManager fasManager, string strName) : base(fasManager, strName)
    {

    }
    public override void OnLoad()
    {
        ES3.Init();
        //初始化log
        GameFrameworkLog.SetLogHelper(new DefaultLogHelper());
        NotificationManager.Instance.OnInit();
        GameManager.Instance.OnInit();
        UIManager.Instance.OnInit();

    }
    public override void OnEnter()
    {
        ChangeState(FsmManagerGame.GAME_STATE_PRELOAD);
    }

    public override void OnRelease()
    {
        GameManager.Instance.OnRelease();
        UIManager.Instance.OnRelease();
        TaskAsynManager.Instance.OnRelease();
        NotificationManager.Instance.OnRelease();
        TransparentNode.ReleaseInstance();
        base.OnRelease();
    }


}
