

public interface ITaskAsyncable
{
    /// <summary>
    /// 获取ID
    /// </summary>
    int TaskId { get; }

    /// <summary>
    /// 状态。
    /// </summary>
    TaskAsynStatus TaskStatus { get; }

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
public enum TaskAsynStatus
{
    None = 0,
    /// <summary>
    /// 开始运行
    /// </summary>
    Run,
    /// <summary>
    /// 运行中
    /// </summary>
    Running,
    /// <summary>
    /// 开始暂停
    /// </summary>
    Suspend,
    /// <summary>
    /// 暂停中
    /// </summary>
    Suspended,
    Stop,
}