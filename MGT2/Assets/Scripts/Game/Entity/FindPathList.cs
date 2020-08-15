using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathList
{
    private ASMapFindPathData _pathData;
    private List<Vector3> _listPath;
    private Vector3 _posEnd;
    private Vector3 _posStart;
    public int Index { get; private set; }
    public void Initial(Vector3 start, Vector3 end)
    {
        _posEnd = end;
        _posStart = start;
        SetIdx(1);
        _pathData = FindPathManager.Instance.FindPathNearest(start, end);
    }


    private bool IsInit()
    {
        return _pathData != null;
    }
    //public bool IsDone()
    //{
    //    return _abPath.IsDone();
    //}
    public bool IsInitListPath(bool useSmooth = true)
    {
        bool isFirst = false;
        return IsInitListPath(out isFirst, useSmooth);
    }
    public bool IsInitListPath(out bool isFirst, bool useSmooth = true)
    {
        isFirst = false;
        if (!IsInit())
        {
            return false;
        }
        if (_listPath == null && _pathData.IsDone())
        {
            _listPath = FindPathManager.Instance.ConverNodeToVectors(_pathData.ListNode);
            isFirst = true;
        }
        return _listPath != null;
    }
    public bool HasIndex(int idx)
    {
        if (idx > -1 && idx < _listPath.Count)
        {
            return true;
        }
        return false;
    }
    public Vector3 GetPosition()
    {
        return _listPath[Index];
    }

    public bool TryGetPosByIdx(out Vector3 pos)
    {
        if (HasIndex(Index))
        {
            pos = _listPath[Index];
            return true;
        }
        pos = Vector3.zero;
        return false;
    }


    public void SetIdx(int idx)
    {
        Index = idx;
    }
    public void Reset()
    {
        _pathData = null;
        _listPath = null;
    }
}
