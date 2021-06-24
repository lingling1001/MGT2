using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ASMapDrawView : MonoBehaviour
{
    private ASMap _mapInfo;
    public ASMap MapInfo { get { return _mapInfo; } }
    public bool IsShowGizmos = true;
    public void SetAsMapInfo(ASMap map)
    {
        _mapInfo = map;
    }

    private void OnDrawGizmos()
    {
        if (MapInfo == null || !IsShowGizmos)
        {
            return;
        }
        DrawMapGrid();


    }

    private void DrawMapGrid()
    {

        float gridSize = ASMapHelper.GetNodeSize(MapInfo);
        float gridSizeHalf = gridSize / 2;

        //Vector3 mapOffset = ASMapHelper.GetOffsetPosition(MapInfo, gridSizeHalf);
        Vector3 mapSize = new Vector3(gridSize * MapInfo.MapSizeX, 0.1f, gridSize * MapInfo.MapSizeY);

        Vector3 mapPos = new Vector3(mapSize.x / 2, 0, mapSize.z / 2);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(mapPos, mapSize);
        Gizmos.color = Color.white;

        if (_mapInfo == null)
        {
            return;
        }
        Vector3 scale = new Vector3(gridSize, 0.1f, gridSize);
        foreach (var item in _mapInfo.GetASNodes())
        {
            Gizmos.color = GetColor(item);
            Vector3 center = ASMapHelper.GetCenterPoaByXY(item.x, item.y, gridSize);
            Gizmos.DrawWireCube(center, scale);

        }



    }
    private Color GetColor(ASNode node)
    {
        if (!node.CanWalk)
        {
            return Color.red;
        }
        if (ListPath.Contains(node.index))
        {
            return Color.blue;
        }
        return Color.white;
    }

    public static List<int> ListPath = new List<int>();
    public static void SetList(List<ASNode> list)
    {
        ListPath.Clear();
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            ListPath.Add(list[cnt].index);
        }
    }


}
