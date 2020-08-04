namespace MFrameWork
{
    /*
     *描述模块的统一加载方式和状态 
     */
    public interface ILoad
    {
        void Load();
        void Release();
        EnumObjectState State { get; }
    }
}