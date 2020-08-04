using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Diagnostics;

public class TaskAsyncThreadFrame : ITaskAsyncable
{
    public int TaskId { get; private set; }

    public TaskAsynStatus TaskStatus { get; private set; }

    private Thread _thread;
    private EventWaitHandle _eventWait = new EventWaitHandle(false, EventResetMode.ManualReset);
    private CalcIntervalTime _calcInterval = new CalcIntervalTime();
    private Stopwatch _stopWatch = new Stopwatch();
    private long _intervalTicks;
    private long _curFrameCount;
    public long CurFrameCount { get { return _curFrameCount; } }

    private long _tempTime;
    public TaskAsyncThreadFrame(int taskId, int intervalMs)
    {
        TaskId = taskId;
        _thread = new Thread(Execute);
        SetRunStatus(TaskAsynStatus.None);
        SetIntervalMs(intervalMs);
    }

    public bool Start()
    {
        if (TaskStatus == TaskAsynStatus.None)
        {
            SetRunStatus(TaskAsynStatus.Running);
            _curFrameCount = 0;
            _stopWatch.Start();
            _calcInterval.Start(_stopWatch.ElapsedTicks, _intervalTicks);
            _thread.Start();
            return true;
        }
        return false;
    }

    public bool Suspend()
    {
        SetRunStatus(TaskAsynStatus.Suspended);
        return true;

    }

    public bool Resume()
    {
        if (TaskStatus == TaskAsynStatus.Suspended)
        {
            _eventWait.Set();
            _stopWatch.Start();
            SetRunStatus(TaskAsynStatus.Running);
            return true;
        }
        return false;
    }

    public void Stop()
    {
        _stopWatch.Stop();
        SetRunStatus(TaskAsynStatus.Stop);
    }


    protected void SetRunStatus(TaskAsynStatus status)
    {
        TaskStatus = status;
    }

    /// <summary>
    /// Sets the interval ms.设置 每秒刷新间隔
    /// </summary>
    public void SetIntervalMs(int intervalMs)
    {
        _intervalTicks = intervalMs * System.TimeSpan.TicksPerMillisecond;
    }

    private void Execute()
    {
        while (true)
        {
            if (TaskStatus == TaskAsynStatus.Running)
            {
                _tempTime = _calcInterval.ExecuteSubValue(_stopWatch.ElapsedTicks);
                if (_tempTime < 0)
                {
                    _calcInterval.Start(_stopWatch.ElapsedTicks, _intervalTicks - _tempTime);
                    _curFrameCount++;
                    OnExecute(CurFrameCount, _stopWatch.ElapsedTicks);
                }
            }
            else if (TaskStatus == TaskAsynStatus.Suspended)
            {
                _eventWait.WaitOne();
                continue;
            }
            else if (TaskStatus == TaskAsynStatus.Stop)
            {
                break;
            }
        }
        _thread.Abort();
    }

    protected virtual void OnExecute(long frameCount, long ticks)
    {



    }
}
