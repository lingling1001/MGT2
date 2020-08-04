using MFrameWork;
using System;

public class APreformCD : APreformBase
{
    /// <summary>
    /// 实际间隔 1秒
    /// </summary>
    public float Value { get; private set; }
    private DateTime _tempValue = DateTime.MinValue;
    public override void OnInitial(EnumAPreform type, AssemblyRole owner, string param)
    {
        base.OnInitial(type, owner, param);
        string[] strParam = Utility.Xml.ParseString<string>(Param, Utility.Xml.SplitComma);
        if (strParam == null || strParam.Length == 0)
        {
            return;
        }
        int value;
        if (int.TryParse(strParam[0], out value))
        {
            Value = value;
        }
        else
        {
            Value = 1;
        }
    }

    public override void OnExecute()
    {
        _tempValue = DateTime.Now.AddMilliseconds(Value);
    }

    public override bool OnCheckPreform()
    {
        return _tempValue < DateTime.Now;
    }
}
