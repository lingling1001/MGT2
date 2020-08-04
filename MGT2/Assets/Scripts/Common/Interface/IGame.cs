namespace MFrameWork
{
    /*
     * 游戏核心对象接口
     */
    public interface IGame
    {
        //游戏当前状态
        EnumObjectState State { get; }

        //开始游戏
        void Run();
        //退出游戏
        void Exit();

        //添加游戏update对象
        void RegisterUpdateObject(IUpdate UpdateObject);
        //移除游戏update对象
        void UnRegisterUpdateObject(IUpdate UpdateObject);

        //讨论
        //是否需要暴露Scene对象??
        //是否需要暴露UI对象??
    }
    /// <summary>
    /// 对象当前状态 
    /// </summary>
    public enum EnumObjectState
    {
        None,
        Initial,
        Loading,
        Ready,
        Disabled,
        Closing
    }
}