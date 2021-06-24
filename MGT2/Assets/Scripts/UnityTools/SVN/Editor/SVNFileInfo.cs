using UnityEngine;

public class SVNFileInfo
{
    public int Index;
    public int SortValue;
    public EnumSVNFileState State;
    public bool IsSelect { get; private set; }
    public string Name { get; private set; }
    public string Flag { get; private set; }
    public bool IsMetaFile { get; private set; }
    public Object Object;
    public void SetIsSelect(bool value)
    {
        IsSelect = value;
    }
    public void SetName(string strName, string flag)
    {
        Name = strName;
        Flag = flag;
        IsMetaFile = Name.Contains(".meta");
        if (flag == "M")
        {
            SetState(EnumSVNFileState.Mod);
        }
        else if (flag == "A")
        {
            SetState(EnumSVNFileState.Add);
        }
        else if (flag == "D")
        {
            SetState(EnumSVNFileState.Del);
        }
        else
        {
            //Debug.LogError(" other type : " + flag);
            SetState(EnumSVNFileState.None);
        }
        SetSortValue((int)State);
    }
    public void ResetSortValue()
    {
        SetSortValue((int)State);
    }
    public void SetSortValue(int value)
    {
        SortValue = value;
    }
    private void SetState(EnumSVNFileState state)
    {
        State = state;
    }
    public override string ToString()
    {
        return Name;
    }

  
}


