namespace MFrameWork
{
    public enum EnumFixedUpdateOrder : int
    {
        First = 1,
        Second = 2,
        Third = 3,
        Four = 4,
    }
    /*
     * 游戏循环的更新操作
     */
    public interface IFixedUpdate
    {
        EnumFixedUpdateOrder Order { get; }
        void OnFixedUpdate(float ElapsedSeconds);
    }
}