using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ASMap
{
    private ASNode[,] _map;
    private int _mapX;
    private int _mapY;
    private int _gridSize;

    /// <summary>
    /// 地图宽度
    /// </summary>
    public int MapSizeX { get { return _mapX; } }
    /// <summary>
    /// 地图长度
    /// </summary>
    public int MapSizeY { get { return _mapY; } }
    /// <summary>
    /// 格子大小，
    /// </summary>
    public int GridSize { get { return _gridSize; } }
    private List<ASNode> _findPathRes = new List<ASNode>();
    public List<ASNode> FindPathRes { get { return _findPathRes; } }

    public void InitialMap(ASNode[,] map, int x, int y, int gridSize)
    {
        _mapX = x;
        _mapY = y;
        _gridSize = gridSize;
        _map = map;
    }

    public ASNode[,] GetASNodes()
    {
        return _map;
    }

    public void FindPath(int[] start, int[] end)
    {
        ASNode nodeStart = GetNode(start);
        ASNode nodeEnd = GetNode(end);
        FindPath(nodeStart, nodeEnd);

    }
    public void FindPath(ASNode nodeStart, ASNode nodeEnd)
    {
        _findPathRes.Clear();
        if (nodeStart == null || nodeEnd == null)
        {
            return;
        }
        List<ASNode> openList = new List<ASNode>();
        HashSet<ASNode> closeList = new HashSet<ASNode>();
        openList.Add(nodeStart);
        while (openList.Count > 0)
        {
            ASNode curNode = openList[0];
            openList.RemoveAt(0);

            closeList.Add(curNode);
            if (curNode == nodeEnd)
            {
                SetFindPath(nodeStart, nodeEnd);
            }

            List<ASNode> rounds = GetNodeRoundsFour(curNode);

            foreach (var item in rounds)
            {
                //处于关闭列表中
                if (closeList.Contains(item))
                {
                    continue;
                }
                int newG = curNode.G + GetDistance(curNode, item);
                if (openList.Contains(item))
                {
                    if (newG < item.G)
                    {
                        item.G = newG;
                        item.Root = curNode;
                    }
                }
                else
                {
                    item.G = newG;
                    item.H = GetDistance(item, nodeEnd);
                    item.Root = curNode;
                    openList.Add(item);
                    openList.Sort((x, y) => x.F.CompareTo(y.F));
                }

            }
            closeList.Add(curNode);
        }

    }
    private void SetFindPath(ASNode start, ASNode end)
    {
        ASNode temp = end;
        while (temp != start)
        {
            _findPathRes.Add(temp);
            temp = temp.Root;
        }
        _findPathRes.Reverse();
    }
    /// <summary>
    /// 获取两点之间的距离
    /// </summary>
    public int GetDistance(ASNode start, ASNode end)
    {
        //int x = Mathf.Abs(start.x - end.x);
        //int y = Mathf.Abs(start.y - end.y);
        //if (x > y)
        //{
        //    return 14 * y + 10 * (x - y);
        //}
        //return 14 * x + 10 * (y - x);

        return Mathf.Abs(start.x - end.x) * 10 + Mathf.Abs(start.y - end.y);
    }


    public List<ASNode> GetNodeRoundsFour(ASNode node)
    {
        List<ASNode> rounds = new List<ASNode>();
        //8方向
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                if (Mathf.Abs(i) == 1 && Mathf.Abs(j) == 1)
                {
                    continue;
                }
                int x = node.x + i;
                int y = node.y + j;
                ASNode roundNode = GetNode(x, y);
                if (roundNode != null && roundNode.CanWalk)
                {
                    rounds.Add(roundNode);
                }
            }
        }
        return rounds;
    }
    /// <summary>
    /// 获取周围点
    /// </summary>
    public List<ASNode> GetNodeRoundsEight(ASNode node)
    {
        List<ASNode> rounds = new List<ASNode>();
        //8方向
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int x = node.x + i;
                int y = node.y + j;
                ASNode roundNode = GetNode(x, y);
                if (roundNode != null && roundNode.CanWalk)
                {
                    rounds.Add(roundNode);
                }
            }
        }
        return rounds;
    }


    /// <summary>
    /// 获取周围一个不是阻挡的点
    /// </summary>
    public ASNode GetNodeNearest(ASNode node, int range = 5)
    {
        if (node.CanWalk)
        {
            return node;
        }
        ASNode curNode;
        int temp = 0;
        while (range > 0)
        {
            List<int[]> list = ASMapHelper.GetNearestGrid(new int[] { node.x, node.y }, temp);
            for (int cnt = 0; cnt < list.Count; cnt++)
            {
                curNode = GetNode(list[cnt]);
                if (curNode != null && curNode.CanWalk)
                {
                    return curNode;
                }
            }
            range--;
            temp++;
        }
        return null;
    }
    public ASNode GetNode(int[] xy)
    {
        return GetNode(xy[0], xy[1]);
    }
    public ASNode GetNode(int x, int y)
    {
        if (x > -1 && x < _map.GetLength(0) && y > -1 && y < _map.GetLength(1))
        {
            return _map[x, y];
        }
        return null;
    }


}
