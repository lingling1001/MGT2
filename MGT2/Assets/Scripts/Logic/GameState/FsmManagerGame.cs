using MFrameWork;


public class FsmManagerGame : FsmManager
{
    public const string GAME_STATE_INIT = "GAME_STATE_INIT";
    public const string GAME_STATE_PRELOAD = "GAME_STATE_PRELOAD";
    public const string GAME_STATE_MAIN = "GAME_STATE_MAIN";
    public const string GAME_STATE_SELECT_ROLE = "GAME_STATE_SELECT_ROLE";
    public const string GAME_STATE_START = "GAME_STATE_START";

    public override void OnInit()
    {
        AddFsm(new FsmGameStateInit(this, GAME_STATE_INIT));//初始化
        AddFsm(new FsmGameStatePreLoad(this, GAME_STATE_PRELOAD));//预加载
        AddFsm(new FsmGameStateMain(this, GAME_STATE_MAIN));//开始界面
        AddFsm(new FsmGameStateSelectRole(this, GAME_STATE_SELECT_ROLE));//选择角色
        AddFsm(new FsmGameStateStart(this, GAME_STATE_START));//开始中

        ChangeState(GAME_STATE_INIT);
    }
}
