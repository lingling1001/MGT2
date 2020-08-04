namespace MFrameWork
{
    /// <summary>
    /// 有限状态机基类。
    /// </summary>
    public abstract class FsmBase : IFsm
    {
        private readonly string _strName;
        /// <summary>
        /// 获取有限状态机名称。
        /// </summary>
        public string Name { get { return _strName; } }

        private readonly FsmManager _fsmManagerInfo;
        public FsmManager FsmManagerInfo { get { return _fsmManagerInfo; } }
        /// <summary>
        /// 初始化有限状态机基类的新实例。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        public FsmBase(FsmManager fsmManager, string strName)
        {
            _fsmManagerInfo = fsmManager;
            _strName = strName;
        }
        public bool ChangeState(string strName)
        {
            if (FsmManagerInfo != null)
            {
                if (CanChange(strName))
                {
                    FsmManagerInfo.ChangeState(strName);
                    return true;
                }
            }
            return false;
        }
        public virtual bool CanChange(string strName)
        {
            return true;
        }
        public virtual void OnLoad()
        {

        }
        public virtual void OnEnter()
        {

        }

        public virtual void OnLeave()
        {

        }

        public virtual void OnInit()
        {

        }

        public virtual void OnRelease()
        {

        }

    }
}