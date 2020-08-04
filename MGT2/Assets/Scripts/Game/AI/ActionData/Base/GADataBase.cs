using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GADataBase
{
    /// <summary>
    /// 类型
    /// </summary>
    public EnumGADType ActionType { get; private set; }
    public bool Value { get; private set; }

    public void SetType(EnumGADType type)
    {
        ActionType = type;
    }
    public void SetValue(bool val)
    {
        Value = val;
    }
}
