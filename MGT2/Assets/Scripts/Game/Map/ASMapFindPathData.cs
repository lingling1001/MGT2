using System.Collections.Generic;

public class ASMapFindPathData
{
    public int Id;
    public int State;
    public int[] Start;
    public int[] End;

    public List<ASNode> ListNode = new List<ASNode>();

    public void SetData(int[] start, int[] end)
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

}