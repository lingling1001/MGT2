

public interface ITaskAsyncable
{
    /// <summary>
    /// 获取ID
    /// </summary>
    int TaskId{ get; }

    /// <summary>
    /// 状态。
    /// </summary>
    TaskAsynStatus TaskStatus{ get; }

    /// <summary> 
    /// 开始运行
    /// </summary> 
    bool Start();

    /// <summary>
    /// 暂停 
    /// </summary>
    bool Suspend();

    /// <summary>
    /// 恢复
    /// </summary>
    bool Resume();

    /// <summary> 
    /// 关闭执行
    /// </summary> 
    void Stop();



}
public  enum TaskAsynStatus
{
    None = 0,
    Running = 1,
    Suspended = 2,
    Stop = 3,
}