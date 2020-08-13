using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASMakeManager : MonoBehaviour
{
    public Vector2Int MapSize;
    /// <summary>
    /// 十分制  
    /// </summary>
    public int NodeSize = 10;
    public LayerMask ObsLayer;

    [HideInInspector]
    public bool IsShowGizmos = true;
    public ASNode[,] Map { get { return _map; } }

    private ASNode[,] _map;

    public void RefreshMap()
    {
        _map = new ASNode[MapSize.x, MapSize.y];
        for (int cntY = 0; cntY < MapSize.x; cntY++)
        {
            for (int cntX = 0; cntX < MapSize.y; cntX++)
            {
                Vector3 pos = GetVector3ByIdx(cntX, cntY);
                bool canWalk = !Physics.CheckBox(pos, Vector3.one * GetNodeSize() / 2 * 0.95f, Quaternion.identity, ObsLayer);
                _map[cntX, cntY] = new ASNode(cntX, cntY, canWalk);
            }
        }

    }

    private float GetNodeSize()
    {
        return NodeSize / 10.0f;
    }
    private Vector3 GetVector3ByIdx(int x, int y)
    {
        Vector3 pos = new Vector3(x * GetNodeSize(), 0, y * GetNodeSize());

        return pos;
    }
    private void OnDrawGizmos()
    {
        if (!IsShowGizmos)
        {
            return;
        }
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(MapSize.x, 1, MapSize.y));
        if (_map == null)
        {
            return;
        }
        float sc = (GetNodeSize());
        Vector3 scale = new Vector3(sc, sc, sc);
        foreach (var item in _map)
        {
            if (!item.CanWalk)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Vector3 center = GetVector3ByIdx(item.x, item.y);

            Gizmos.DrawWireCube(center, scale);
        }



    }






    //private ASNode[,] _map;
    //public int MapSizeX = 100;
    //public int MapSizeY = 100;
    //public float NodeSize = 0.5f;
    //public Vector3 NodeStartPos = Vector3.zero;
    //public int MapSize { get { return MapSizeX * MapSizeY; } }
    //public float MapMaxSizeX { get { return MapSizeX * NodeSize; } }
    //public float MapMaxSizeY { get { return MapSizeX * NodeSize; } }

    //public LayerMask ObsLayer;
    //public List<ASNode> Path = new List<ASNode>();
    //public Transform player;
    //public Transform enemy;

    //private bool isFinish = true;

    //public TextMesh prefabTxt;
    //private WaitForSeconds step = new WaitForSeconds(0.1f);
    //private float startPosX { get { return MapSizeX * NodeSize / 2; } }
    //private float startPosY { get { return MapSizeY * NodeSize / 2; } }
    ////private void Start()
    ////{
    ////    GenerateMap();
    ////}
    //private void Update()
    //{
    //    if (player == null || enemy == null)
    //    {
    //        return;
    //    }
    //    if (isFinish)
    //    {


    //        StartCoroutine(FindPath(player.position, enemy.position));

    //        foreach (var item in _map)
    //        {
    //            Vector3 center = new Vector3(-item.x * NodeSize - NodeSize / 2, 0, -item.y * NodeSize - NodeSize / 2);
    //            TextMesh txt = GetTextMesh();
    //            txt.transform.position = center + Vector3.up * 2;
    //            txt.text = item.G.ToString();
    //            txt.color = Color.white;
    //        }
    //        foreach (var item in Path)
    //        {
    //            Vector3 center = new Vector3(-item.x * NodeSize - NodeSize / 2, 0, -item.y * NodeSize - NodeSize / 2);
    //            TextMesh txt = GetTextMesh();
    //            txt.transform.position = center + Vector3.up * 2.3f;
    //            txt.text = item.G.ToString();
    //            txt.color = Color.green;
    //        }
    //        ResetTxt();

    //    }

    //}
    //public void GenerateMap()
    //{
    //    _map = new ASNode[MapSizeX, MapSizeY];
    //    float startPosX = MapSizeX * NodeSize / 2;
    //    float startPosY = MapSizeY * NodeSize / 2;
    //    for (int cntY = 0; cntY < MapSizeY; cntY++)
    //    {
    //        for (int cntX = 0; cntX < MapSizeX; cntX++)
    //        {
    //            Vector3 pos = new Vector3(-cntX * NodeSize - NodeSize / 2, 0, -cntY * NodeSize - NodeSize / 2);

    //            bool canWalk = !Physics.CheckBox(pos, Vector3.one * NodeSize / 2 * 0.9f, Quaternion.identity, ObsLayer);
    //            _map[cntX, cntY] = new ASNode(cntX, cntY, canWalk);


    //            //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //            //obj.transform.position = pos;
    //            //obj.transform.SetParent(transform);
    //            //obj.transform.localScale = Vector3.one * 0.9f;
    //            //obj.name = cntX + " " + cntY;
    //        }
    //    }

    //}

    //public IEnumerator FindPath(Vector3 start, Vector3 end)
    //{

    //    Path.Clear();
    //    foreach (var item in _map)
    //    {
    //        item.G = 0;
    //        item.Root = null;
    //    }
    //    ASNode nodeStart = GetNodeByPos(start);
    //    ASNode nodeEnd = GetNodeByPos(end);

    //    List<ASNode> openList = new List<ASNode>();

    //    HashSet<ASNode> closeList = new HashSet<ASNode>();
    //    openList.Add(nodeStart);

    //    while (openList.Count > 0)
    //    {
    //        ASNode curNode = openList[0];
    //        openList.RemoveAt(0);

    //        closeList.Add(curNode);
    //        if (curNode == nodeEnd)
    //        {
    //            SetFindPath(nodeStart, nodeEnd);
    //        }

    //        List<ASNode> rounds = GetNodeRoundsFour(curNode);

    //        foreach (var item in rounds)
    //        {
    //            //处于关闭列表中
    //            if (closeList.Contains(item))
    //            {
    //                continue;
    //            }
    //            int newG = curNode.G + GetDistance(curNode, item);
    //            if (openList.Contains(item))
    //            {
    //                if (newG < item.G)
    //                {
    //                    item.G = newG;
    //                    item.Root = curNode;
    //                }
    //            }
    //            else
    //            {
    //                item.G = newG;
    //                item.H = GetDistance(item, nodeEnd);
    //                item.Root = curNode;
    //                openList.Add(item);
    //                openList.Sort((x, y) => x.F.CompareTo(y.F));

    //            }

    //        }
    //        closeList.Add(curNode);
    //    }
    //    yield return null;
    //}

    //public int GetDistance(ASNode start, ASNode end)
    //{
    //    int x = Mathf.Abs(start.x - end.x);
    //    int y = Mathf.Abs(start.y - end.y);
    //    if (x > y)
    //    {
    //        return 14 * y + 10 * (x - y);
    //    }
    //    return 14 * x + 10 * (y - x);
    //    return Mathf.Abs(start.x - end.x) * 10 + Mathf.Abs(start.y - end.y);
    //}

    //public void SetFindPath(ASNode start, ASNode end)
    //{
    //    ASNode temp = end;
    //    while (temp != start)
    //    {
    //        Path.Add(temp);
    //        temp = temp.Root;
    //    }
    //    Path.Reverse();

    //}
    //public List<ASNode> GetNodeRoundsFour(ASNode node)
    //{
    //    List<ASNode> rounds = new List<ASNode>();
    //    //8方向
    //    for (int i = -1; i < 2; i++)
    //    {
    //        for (int j = -1; j < 2; j++)
    //        {
    //            if (i == 0 && j == 0)
    //            {
    //                continue;
    //            }
    //            if (Mathf.Abs(i) == 1 && Mathf.Abs(j) == 1)
    //            {
    //                continue;
    //            }
    //            int x = node.x + i;
    //            int y = node.y + j;
    //            if (x >= 0 && y >= 0 && x < MapSizeX && y < MapSizeY)
    //            {
    //                if (_map[x, y].CanWalk)
    //                {
    //                    rounds.Add(_map[x, y]);
    //                }
    //            }
    //        }
    //    }
    //    return rounds;
    //}
    ///// <summary>
    ///// 获取周围点
    ///// </summary>
    //public List<ASNode> GetNodeRoundsEight(ASNode node)
    //{
    //    List<ASNode> rounds = new List<ASNode>();
    //    //8方向
    //    for (int i = -1; i < 2; i++)
    //    {
    //        for (int j = -1; j < 2; j++)
    //        {
    //            if (i == 0 && j == 0)
    //            {
    //                continue;
    //            }
    //            int x = node.x + i;
    //            int y = node.y + j;
    //            if (x >= 0 && y >= 0 && x < MapSizeX && y < MapSizeY)
    //            {
    //                if (_map[x, y].CanWalk)
    //                {
    //                    rounds.Add(_map[x, y]);
    //                }
    //            }
    //        }
    //    }
    //    return rounds;
    //}


    //public ASNode GetNodeByPos(Vector3 pos)
    //{


    //    float tempX = Mathf.Abs(pos.x / NodeSize);
    //    float tempY = Mathf.Abs(pos.z / NodeSize);


    //    int x = Mathf.FloorToInt(tempX);
    //    int y = Mathf.FloorToInt(tempY);


    //    return _map[x, y];
    //}

    //private void OnDrawGizmos()
    //{
    //    Vector3 posCenter = new Vector3(NodeStartPos.x - MapMaxSizeX / 2, 0, NodeStartPos.y - MapMaxSizeY / 2);

    //    Gizmos.DrawWireCube(posCenter, new Vector3(MapSizeX * NodeSize * 1.1f, 1, MapSizeY * NodeSize * 1.1f));
    //    Gizmos.color = Color.red;
    //    if (_map == null)
    //    {
    //        return;
    //    }

    //    float sc = (NodeSize * 0.9f);
    //    Vector3 scale = new Vector3(sc, sc, sc);

    //    foreach (var item in _map)
    //    {
    //        if (!item.CanWalk)
    //        {
    //            Gizmos.color = Color.red;
    //        }
    //        else
    //        {
    //            Gizmos.color = Color.white;
    //        }
    //        Vector3 center = new Vector3(-item.x * NodeSize - NodeSize / 2, 0, -item.y * NodeSize - NodeSize / 2);
    //        Gizmos.DrawWireCube(center, scale);
    //    }
    //    return;
    //    //foreach (var item in PathArrounds)
    //    //{
    //    //    Gizmos.color = Color.gray;
    //    //    Vector3 center = new Vector3(item.x * NodeSize + startPosX, 0, item.y * NodeSize + startPosY);
    //    //    Gizmos.DrawCube(center, scale);
    //    //}

    //    //foreach (var item in PathPoints)
    //    //{
    //    //    Gizmos.color = Color.green;
    //    //    Vector3 center = new Vector3(item.x * NodeSize + startPosX, 0, item.y * NodeSize + startPosY);
    //    //    Gizmos.DrawCube(center, scale);
    //    //}
    //    //foreach (var item in CurrentNode)
    //    //{
    //    //    Gizmos.color = Color.yellow;
    //    //    Vector3 center = new Vector3(item.x * NodeSize + startPosX, 0, item.y * NodeSize + startPosY);
    //    //    Gizmos.DrawCube(center, scale);
    //    //}
    //    //foreach (var item in Path)
    //    //{
    //    //    Gizmos.color = Color.green;
    //    //    Vector3 center = new Vector3(item.x * NodeSize + startPosX, 0, item.y * NodeSize + startPosY);
    //    //    Gizmos.DrawCube(center, scale);

    //    //}
    //}
    //private List<TextMesh> _temp = new List<TextMesh>();
    //private List<TextMesh> _use = new List<TextMesh>();

    //private TextMesh GetTextMesh()
    //{
    //    TextMesh txt = null;
    //    if (_temp.Count > 0)
    //    {
    //        txt = _temp[0];
    //        _temp.RemoveAt(0);
    //    }
    //    else
    //    {
    //        txt = GameObject.Instantiate(prefabTxt);
    //    }
    //    _use.Add(txt);

    //    return txt;
    //}
    //private void ResetTxt()
    //{
    //    _temp.AddRange(_use);
    //    _use.Clear();
    //}
}
