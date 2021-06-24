using System.Collections.Generic;
using UnityEngine;

public class ASMapHelper
{

    public static float GetNodeSize(ASMap map)
    {
        return GetNodeSize(map.GridSize);
    }
    public static float GetNodeSize(int gridSize)
    {
        return gridSize / 10.0f;
    }

    public static Vector3 GetCenterPoaByXY(int x, int y, float gridSize)
    {
        return new Vector3(x * gridSize + gridSize / 2, 0, y * gridSize + gridSize / 2);
    }
    public static Vector3 ConverNodeToVector(ASNode node, ASMap map)
    {
        float gridSize = GetNodeSize(map);
        return ConverNodeToVector(node, gridSize);
    }
    public static Vector3 ConverNodeToVector(ASNode node, float gridSize)
    {
        return new Vector3(node.x * gridSize, 0, node.y * gridSize);
    }
    /// <summary>
    /// 文本转换成地图信息
    /// </summary>
    public static ASNode[,] ConvertTxtToMap(string strInfos, int sx, int sy)
    {
        ASNode[,] map = new ASNode[sx, sy];
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                map[x, y] = new ASNode(x, y, sx, strInfos[y * sx + x] == '1');
            }
        }
        return map;
    }
    /// <summary>
    /// 转换地图信息成文本
    /// </summary>
    public static string ConvertMapToTxt(ASNode[,] map)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int maxY = map.GetLength(1);
        int maxX = map.GetLength(0);
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)//先保存x行
            {
                sb.Append(map[x, y].CanWalk ? "1" : "0");
            }
        }
        return sb.ToString();

    }

    public static List<Vector3> ConverNodeToVectors(List<ASNode> list, float gridSize, int rate = 2)
    {
        List<Vector3> listPos = new List<Vector3>();
        if (list.Count == 0)
        {
            return listPos;
        }
        int count = list.Count - 1;
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            if (cnt % rate == 0)
            {
                listPos.Add(ConverNodeToVector(list[cnt], gridSize));
            }
        }
        listPos.Add(ConverNodeToVector(list[count], gridSize));
        return listPos;
    }


    /// <summary>
    /// 获取周围格子信息
    /// </summary>
    /// <param name="num">范围</param>
    /// <returns>格子信息</returns>
    public static List<int[]> GetNearestGrid(int[] xy, int num)
    {
        List<int[]> arrs = new List<int[]>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int newX = 0, newY = 0;
                if (x > 0)
                {
                    newX = num;
                }
                else if (x < 0)
                {
                    newX = -num;
                }
                if (y > 0)
                {
                    newY = num;
                }
                else if (y < 0)
                {
                    newY = -num;
                }
                arrs.Add(new[] { newX, newY });
            }
        }
        return arrs;
    }


}
//public static Vector3 GetOffsetPosition(ASMap map, float gridHalfSize)
//{
//    return GetOffsetPosition(map.MapSizeX, map.MapSizeY, gridHalfSize);
//}
//public static Vector3 GetOffsetPosition(int x, int y, float gridHalfSize)
//{
//    return new Vector3(x * gridHalfSize, 0, y * gridHalfSize);
//}