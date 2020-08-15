using MFrameWork;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FindPathManager : Singleton<FindPathManager>
{
    private List<ASMapFindPathData> _listCache = new List<ASMapFindPathData>();
    private AStarThread _astartThread;
    public AStarThread AstartThread { get { return _astartThread; } }
    /// <summary>
    /// 刷新地图信息
    /// </summary>
    public void RefreshMapInfo(PrototypeMap data)
    {
        if (_astartThread == null)
        {
            _astartThread = new AStarThread(1, 10);
            TaskAsynManager.Instance.AdditionTask(_astartThread);
        }
        ASMap mapInfo = new ASMap();
        TextAsset txt = ResLoadHelper.LoadAsset<TextAsset>(data.StarInfo);
        if (txt == null)
        {
            return;
        }
        int x = data.GetSize()[0] * 10 / data.GridSize;
        int y = data.GetSize()[1] * 10 / data.GridSize;
        ASNode[,] nodes = ASMapHelper.ConvertTxtToMap(txt.text, x, y);
        mapInfo.InitialMap(nodes, x, y, data.GridSize);
        _astartThread.RefreshMapInfo(mapInfo);

    }
    public ASMapFindPathData FindPathNearest(ASNode start, ASNode endNode)
    {
        return _astartThread.AddFindPath(start, endNode);
    }
    /// <summary>
    /// 获取路径信息
    /// </summary>
    public ASMapFindPathData FindPathNearest(Vector3 start, Vector3 end)
    {
        ASNode nodeStart = _astartThread.MapInfo.GetNode(ConvertPosition(start));
        ASNode nodeEnd = _astartThread.MapInfo.GetNode(ConvertPosition(end));
        ASNode nearestEnd = _astartThread.MapInfo.GetNodeNearest(nodeEnd);
        return FindPathNearest(nodeStart, nearestEnd);
    }

    public List<Vector3> ConverNodeToVectors(List<ASNode> list)
    {
        return ASMapHelper.ConverNodeToVectors(list, GetGridSize());
    }

    public bool IsCanWalk(Vector3 pos)
    {
        ASNode node = AstartThread.MapInfo.GetNode(ConvertPosition(pos));
        if (node == null)
        {
            return false;
        }
        return node.CanWalk;

    }
    public int[] ConvertPosition(Vector3 position)
    {
        int[] arrs = new int[2];
        arrs[0] = Mathf.RoundToInt(position.x * GetGridSize());
        arrs[1] = Mathf.RoundToInt(position.z * GetGridSize());
        return arrs;
    }

    public float GetGridSize()
    {
        return _astartThread.GetGridSize() / 10.0f;
    }
    public ASNode[,] GetMapNode()
    {
        return _astartThread.GetMapNode();
    }

    public ASMapFindPathData FindPath(ASNode start, ASNode end)
    {
        return _astartThread.AddFindPath(start, end);
    }

    public ASMapFindPathData CreateData(ASNode start, ASNode end)
    {
        ASMapFindPathData data;
        if (_listCache.Count > 0)
        {
            data = _listCache[0];
            _listCache.RemoveAt(0);
        }
        else
        {
            data = new ASMapFindPathData();
        }
        data.SetData(start, end);
        return data;
    }

    public void ReleaseData(ASMapFindPathData data)
    {
        _listCache.Add(data);

    }
    public void OnRelease()
    {
        TaskAsynManager.Instance.FinishTask(1);
    }


}
