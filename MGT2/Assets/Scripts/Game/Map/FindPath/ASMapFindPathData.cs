using System.Collections.Generic;

public class ASMapFindPathData
{
    public int Id;
    public int State;
    public ASNode Start;
    public ASNode End;

    public List<ASNode> ListNode = new List<ASNode>();

    public void SetData(ASNode start, ASNode end)
    {
        ListNode.Clear();
        SetState(0);
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

}