using MFrameWork;
using System;

public class AEAnimator : AEffectEventBase
{
    /// <summary>
    /// 动画名称
    /// </summary>
    public string _animatorName;
    /// <summary>
    /// 动画时长
    /// </summary>
    private int _animatorTime;
    private DateTime _tempValue = DateTime.MinValue;
    public override void OnInitial(EnumAEffectEvent type, AssemblyRole owner, string param)
    {
        base.OnInitial(type, owner, param);
        string[] strParam = Utility.Xml.ParseString<string>(param, Utility.Xml.SplitComma);
        if (strParam == null)
        {
            return;
        }
        if (strParam.Length > 0)
        {
            _animatorName = strParam[0];
        }
        if (strParam.Length > 1)
        {
            _animatorTime = int.Parse(strParam[1]);
        }
        else
        {
            _animatorTime = 2000;
        }
    }
    public override void Execute()
    {
        base.Execute();
        Owner.AssyAnimator.PlayAnimator(_animatorName);
        _tempValue = DateTime.Now.AddMilliseconds(_animatorTime);
    }
    public override void UpdateEvent()
    {
        if (DateTime.Now > _tempValue)
        {
            SetIsFinish(true);
        }
    }
}
