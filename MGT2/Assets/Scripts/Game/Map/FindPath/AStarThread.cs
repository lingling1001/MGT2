using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarThread : TaskAsyncThreadFrame
{
    public long Frame;
    public long Ticks;
    private ASMap _mapInfo;
    public ASMap MapInfo { get { return _mapInfo; } }

    private List<ASMapFindPathData> _listTask = new List<ASMapFindPathData>();

    public AStarThread(int taskId, int intervalMs) : base(taskId, intervalMs)
    {

    }
    public void RefreshMapInfo(ASMap mapInfo)
    {
        _mapInfo = mapInfo;
    }
    public ASNode[,] GetMapNode()
    {
        return _mapInfo.GetASNodes();
    }
    public int GetGridSize()
    {
        return _mapInfo.GridSize;
    }
    public ASMapFindPathData AddFindPath(ASNode start, ASNode end)
    {
        ASMapFindPathData data = FindPathManager.Instance.CreateData(start, end);
        _listTask.Add(data);
        return data;
    }
    protected override void OnExecute(long frameCount, long ticks)
    {
        Frame = frameCount;
        Ticks = ticks;
        if (_listTask.Count > 0)
        {
            ASMapFindPathData data = _listTask[0];
            _mapInfo.FindPath(data.Start, data.End);
            data.ListNode.AddRange(_mapInfo.FindPathRes);
            data.SetState(1);
            _listTask.RemoveAt(0);
        }

    }


}
