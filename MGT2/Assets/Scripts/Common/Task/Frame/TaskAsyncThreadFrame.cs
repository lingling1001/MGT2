﻿using System.Collections;
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
    private Stopwatch _stopWatch = new Stopwatch();
    /// <summary>
    /// 运行间隔 毫秒
    /// </summary>
    private int _intervalMs;
    public int IntervalMs { get { return _intervalMs; } }
    private long _curFrameCount;
    public long CurFrameCount { get { return _curFrameCount; } }

    private long _tempSubTime;

    public event System.Action<long, long> EventExecute;
    /// <summary>
    /// 循环执行
    /// </summary>
    /// <param name="taskId">任务ID</param>
    /// <param name="intervalMs">间隔毫秒</param>
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
            SetRunStatus(TaskAsynStatus.Run);
            _curFrameCount = 0;
            _stopWatch.Restart();
            _thread.Start();
            return true;
        }
        return false;
    }

    public bool Suspend()
    {
        SetRunStatus(TaskAsynStatus.Suspend);
        return true;
    }

    public bool Resume()
    {
        SetRunStatus(TaskAsynStatus.Run);
        return true;

    }


    public void Stop()
    {
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
        _intervalMs = intervalMs;
    }
    private long _tempNum;
    private void Execute()
    {
        while (true)
        {
            if (TaskStatus == TaskAsynStatus.Running)
            {
                Thread.Sleep(IntervalMs / 4);
                //当前时间 减去 上次的时间
                _tempSubTime = _stopWatch.ElapsedMilliseconds - _tempNum;
                if (_tempSubTime > IntervalMs)
                {
                    _curFrameCount = _curFrameCount + _tempSubTime / IntervalMs;
                    //时间差= 当前时间-本次多余出的时间
                    _tempNum = _stopWatch.ElapsedMilliseconds - _tempSubTime % IntervalMs;
                    OnExecute(CurFrameCount, _stopWatch.ElapsedTicks);
                }
            }
            else if (TaskStatus == TaskAsynStatus.Run)
            {
                _eventWait.Set();
                _stopWatch.Start();
                SetRunStatus(TaskAsynStatus.Running);
            }
            else if (TaskStatus == TaskAsynStatus.Suspend)
            {
                _stopWatch.Stop();
                _eventWait.WaitOne();
                SetRunStatus(TaskAsynStatus.Suspended);
            }
            else if (TaskStatus == TaskAsynStatus.Suspended)
            {
                continue;
            }
            else if (TaskStatus == TaskAsynStatus.Stop)
            {
                break;
            }
        }
        _thread.Abort();

    }

    private void OnExecute(long frameCount, long ticks)
    {
        if (EventExecute != null)
        {
            EventExecute(frameCount, ticks);
        }
    }
}
