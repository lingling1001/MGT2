using MFrameWork;
using UnityEngine;

public class FsmGameStateSelectRole : FsmBase
{
    public FsmGameStateSelectRole(FsmManager fasManager, string strName) : base(fasManager, strName)
    {
    }
    public override void OnInit()
    {
        base.OnInit();
        AssyEntityManager.Instance.OnInit();
    }
    public override void OnEnter()
    {
        UIManager.Instance.OpenUI<UISelectRole>(EnumUIType.UISelectRole);
        base.OnEnter();
    }

    public override void OnLeave()
    {
        UIManager.Instance.CloseUI(EnumUIType.UISelectRole);
        base.OnLeave();
    }
    public override void OnRelease()
    {
        base.OnRelease();
        AssyEntityManager.Instance.OnRelease();

    }
}
