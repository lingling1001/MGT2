using System.Collections.Generic;

public class ASMapFindPathData : IMonoPool
{
    public int Id;
    public int State;
    public ASNode Start;
    public ASNode End;

    public List<ASNode> ListNode = new List<ASNode>();

    public string PoolKey { get; set; }

    public void SetData(ASNode start, ASNode end)
    {
        Start = start;
        End = end;
    }
    public void SetState(int state)
    {
        State = state;
    }

    public bool IsDone()
    {
        return State == 1;
    }
    public void EnterPool()
    {
        ListNode.Clear();
        SetState(0);
    }
}