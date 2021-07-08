using UnityEngine;

public class AgentDataPosition : AgentDataBase
{
    public Vector3 Value { get; private set; }
    public void SetValue(Vector3 val)
    {
        Value = val;
    }
}
