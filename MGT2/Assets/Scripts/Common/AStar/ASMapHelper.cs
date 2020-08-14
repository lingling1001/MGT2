public class ASMapHelper
{
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
                map[x, y] = new ASNode(x, y, strInfos[y * sx + x] == '1');
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
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)//先保存x行
            {
                sb.Append(map[x, y].CanWalk ? "1" : "0");
            }
        }
        return sb.ToString();

    }

}
