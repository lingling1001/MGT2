using System;

public class AEDelay : AEffectEventBase
{
    private DateTime _tempValue = DateTime.MinValue;
    private int _delayMs;
    public override void OnInitial(EnumAEffectEvent type, AssemblyRole owner, string strParem)
    {
        base.OnInitial(type, owner, strParem);
        _delayMs = int.Parse(strParem);
    }
    public override void Execute()
    {
        base.Execute();
        _tempValue = DateTime.Now.AddMilliseconds(_delayMs);
    }
    public override void UpdateEvent()
    {
        if (DateTime.Now > _tempValue)
        {
            SetIsFinish(true);
        }
    }

}

