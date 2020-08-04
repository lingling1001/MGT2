using MFrameWork;
using UnityEngine;
public class FsmGameStateMain : FsmBase
{
    private GameObject _entrySource;
    public FsmGameStateMain(FsmManager fasManager, string strName) : base(fasManager, strName)
    {

    }
    public override void OnInit()
    {
        base.OnInit();
        _entrySource = GameObject.Find("EntrySource");
    }
    public override void OnEnter()
    {
        UIManager.Instance.OpenUI<UIEnterGame>(EnumUIType.UIEnterGame);
        UnityObjectExtension.SetActive(_entrySource, true);
    }

    public override void OnLeave()
    {
        UIManager.Instance.CloseUI(EnumUIType.UIEnterGame);
        UnityObjectExtension.SetActive(_entrySource, false);

    }
}