using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDataBase
{
    /// <summary>
    /// 类型
    /// </summary>
    public string ActionType { get; private set; }
    /// <summary>
    /// 当前状态值
    /// </summary>
    public bool State { get; private set; }
    public virtual void Initial(string strType, bool value)
    {
        ActionType = strType;
        State = value;
    }

    public virtual void SetState(bool value)
    {
        State = value;
    }
}
