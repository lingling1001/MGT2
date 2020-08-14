using MFrameWork;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FindPathManager : Singleton<FindPathManager>
{
    private List<ASMapFindPathData> _listCache = new List<ASMapFindPathData>();
    private AStarThread _astartThread;
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
        int x = data.GetSize()[0];
        int y = data.GetSize()[1];
        ASNode[,] nodes = ASMapHelper.ConvertTxtToMap(txt.text, x, y);
        mapInfo.InitialMap(nodes, x, y);
        _astartThread.RefreshMapInfo(mapInfo);

    }

    public ASNode[,] GetMapNode()
    {
        return _astartThread.GetMapNode();
    }
    public ASMapFindPathData FindPath(int[] start, int[] end)
    {
        return _astartThread.AddFindPath(start, end);
    }

    public ASMapFindPathData CreateData(int[] start, int[] end)
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
