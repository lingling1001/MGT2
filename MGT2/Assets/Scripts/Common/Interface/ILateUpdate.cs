namespace MFrameWork
{
    /*
     * 游戏循环的更新操作
     */
    public interface ILateUpdate
    {
        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        int Priority { get; }
        void On_LateUpdate(float elapseSeconds, float realElapseSeconds);

    }
}