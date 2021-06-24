using MFrameWork;
using UnityEngine;
public class FsmGameStateMain : FsmBase
{
    public FsmGameStateMain(FsmManager fasManager, string strName) : base(fasManager, strName)
    {

    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnEnter()
    {
        UIManager.QOpenUI<UIEnterGame>();
        
    }

    public override void OnLeave()
    {
        UIManager.QCloseUI<UIEnterGame>();
        
    }
}