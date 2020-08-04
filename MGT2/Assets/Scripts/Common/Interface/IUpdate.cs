namespace MFrameWork
{
    /*
     * 游戏循环的更新操作
     */
    public interface IUpdate
    {
        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        int Priority { get; }
        /// <summary>
        /// 更新检测
        /// </summary>
        /// <param name="elapseSeconds">间隔</param>
        /// <param name="realElapseSeconds">真实运行时间</param>
        void On_Update(float elapseSeconds, float realElapseSeconds);
    }
}