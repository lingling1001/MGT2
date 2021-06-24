using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathTools
{
    private List<ASMapFindPathData> _listTask = new List<ASMapFindPathData>();
    private TaskAsyncThreadFrame _astartThread;
    private ASMap _mapInfo;
    public ASMap MapInfo { get { return _mapInfo; } }

    private float _realGridSize;
    public Vector3 CenterPosition { get; private set; }

    /// <summary>
    /// 刷新地图数据信息
    /// </summary>
    public void InitialMapInfo(string starInfo, Vector2Int size, int gridSize)
    {
        TextAsset txt = ResLoadHelper.LoadAsset<TextAsset>(starInfo);
        if (txt == null)
        {
            return;
        }
       
        InitialMapInfo(txt.text, size.x, size.y, gridSize);
    }

    public void InitialMapInfo(string txt, int sizeX, int sizeY, int gridSize)
    {
        if (txt == null)
        {
            return;
        }
        if (_astartThread == null)
        {
            int taskId = TaskAsynManager.Instance.GetFreeTaskId();
            _astartThread = new TaskAsyncThreadFrame(taskId, 10);
            _astartThread.EventExecute += OnExecute;
            TaskAsynManager.Instance.AdditionTask(_astartThread);

        }
        _mapInfo = new ASMap();
        ASNode[,] nodes = ASMapHelper.ConvertTxtToMap(txt, sizeX, sizeY);
        _mapInfo.InitialMap(nodes, sizeX, sizeY, gridSize);

        _realGridSize = ASMapHelper.GetNodeSize(_mapInfo);

        CenterPosition = GetCenterPos();
    }


    public ASMapFindPathData FindPathNearest(ASNode start, ASNode endNode)
    {
        return AddFindPath(start, endNode);
    }

    private ASMapFindPathData AddFindPath(ASNode start, ASNode end)
    {
        ASMapFindPathData data = GameManager.QGetOrAddMgr<MapManager>().FindPath.CreateData(start, end);
        _listTask.Add(data);
        return data;
    }
    /// <summary>
    /// 获取路径信息
    /// </summary>
    public ASMapFindPathData FindPathNearest(Vector3 start, Vector3 end, int range = 3)
    {
        ASNode nodeStart = MapInfo.GetNode(ConvertPosition(start));
        ASNode nodeEnd = MapInfo.GetNode(ConvertPosition(end));
        ASNode nearestEnd = MapInfo.GetNodeNearest(nodeEnd, range);
        if (nearestEnd == null || nodeStart == null)
        {
            return null;
        }
        return FindPathNearest(nodeStart, nearestEnd);
    }

    public List<Vector3> ConverNodeToVectors(List<ASNode> list)
    {
        return ASMapHelper.ConverNodeToVectors(list, GetGridSize());
    }

    public bool IsCanWalk(Vector3 pos)
    {
        ASNode node = MapInfo.GetNode(ConvertPosition(pos));
        if (node == null)
        {
            return false;
        }
        return node.CanWalk;

    }
    public int[] ConvertPosition(Vector3 position)
    {
        int[] arrs = new int[2];
        arrs[0] = Mathf.FloorToInt(position.x / GetGridSize());
        arrs[1] = Mathf.FloorToInt(position.z / GetGridSize());
        return arrs;
    }

    public float GetGridSize()
    {
        return _realGridSize;
    }


    public Vector3 GetPositonByIndex(int index)
    {
        ASNode node = MapInfo.GetNode(index);
        if (node == null)
        {
            return Vector3.zero;
        }
        return ASMapHelper.ConverNodeToVector(node, GetGridSize());
    }
    private Vector3 GetCenterPos()
    {
        float x = MapInfo.MapSizeX * GetGridSize() / 2;
        float z = MapInfo.MapSizeY * GetGridSize() / 2;
        return new Vector3(x, 0, z);
    }

    public ASMapFindPathData FindPath(ASNode start, ASNode end)
    {
        return AddFindPath(start, end);
    }
    private void OnExecute(long frameCount, long ticks)
    {
        if (_listTask.Count > 0)
        {
            ASMapFindPathData data = _listTask[0];
            MapInfo.FindPath(data.Start, data.End);
            data.ListNode.AddRange(_mapInfo.FindPathRes);
            data.SetState(1);
            _listTask.Remove(data);
            ItemPoolMgr.Instance.AddPoolItem(data);
        }

    }

    public static string PoolKey = "ASMapFindPathData";

    public ASMapFindPathData CreateData(ASNode start, ASNode end)
    {
        ASMapFindPathData data = ItemPoolMgr.CreateOrGetObj<ASMapFindPathData>(PoolKey);
        data.SetData(start, end);
        return data;
    }


    public void OnRelease()
    {
        if (_astartThread != null)
        {
            TaskAsynManager.Instance.FinishTask(_astartThread.TaskId);
            _astartThread.EventExecute -= OnExecute;
            _astartThread = null;
        }

    }
}
