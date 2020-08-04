namespace MFrameWork
{
    /// <summary>
    /// 有限状态机接口。
    /// </summary>
    public interface IFsm : IInit
    {
        /// <summary>
        /// 获取有限状态机名称。
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 是否可以进入状态。
        /// </summary>
        bool CanChange(string strName);
        void OnLoad();
        void OnEnter();
        void OnLeave();
       

    }
}