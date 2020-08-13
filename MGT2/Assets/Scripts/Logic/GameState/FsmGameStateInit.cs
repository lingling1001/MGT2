using MFrameWork;

public class FsmGameStateInit : FsmBase
{
    public FsmGameStateInit(FsmManager fasManager, string strName) : base(fasManager, strName)
    {
    }
    public override void OnLoad()
    {
        //初始化log
        GameFrameworkLog.SetLogHelper(new DefaultLogHelper());
        NotificationManager.Instance.OnInit();

        ES3.Init();
    }
    public override void OnEnter()
    {
        ChangeState(FsmManagerGame.GAME_STATE_PRELOAD);

    }

    public override void OnRelease()
    {
        TaskAsynManager.Instance.OnRelease();
        NotificationManager.Instance.OnRelease();
        base.OnRelease();
       
    }
}
