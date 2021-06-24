public class ASNode
{
    public int index { get; private set; }
    /// <summary>
    /// x坐标
    /// </summary>
    public int x { get; private set; }
    /// <summary>
    /// y坐标
    /// </summary>
    public int y { get; private set; }

    public ASNode Root;
    /// <summary>
    /// 是否可以行走
    /// </summary>
    public bool CanWalk { get; private set; }
    public int G;
    public int H;
    public int F { get { return G + H; } }
    public ASNode(int x, int y, int xCount, bool canWalk)
    {
        this.x = x;
        this.y = y;
        this.index = y * xCount + x;
        this.CanWalk = canWalk;
    }
    public int CompareTo(ASNode other)
    {
        return F.CompareTo(other.F);
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }
}
