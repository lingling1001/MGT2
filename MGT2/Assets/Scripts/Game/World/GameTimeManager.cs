using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : ManagerBase
{
    public const int SPEED_DEFAULT = 1000;
    public const int SPEED_MAX = 5;

    public int GameSpeed { get; private set; }
    public long RunTime { get; private set; }

    private TaskAsyncThreadFrame _threadTime;

    public override void On_Init()
    {
        int taskId = TaskAsynManager.Instance.GetFreeTaskId();
        _threadTime = new TaskAsyncThreadFrame(taskId, SPEED_DEFAULT);
        TaskAsynManager.Instance.AdditionTask(_threadTime);
        _threadTime.EventExecute += EventExecute;
    }

    private void EventExecute(long arg1, long arg2)
    {
        RunTime = _threadTime.CurFrameCount;
    }

    /// <summary>
    ///  «∑Ò‘›Õ£
    /// </summary>
    public bool IsPause()
    {
        switch (_threadTime.TaskStatus)
        {
            case TaskAsynStatus.Suspend:
            case TaskAsynStatus.Suspended:
                return true;
        }
        return false;
    }

    public void SetPauseState(bool isPause)
    {
        if (isPause)
        {
            _threadTime.Suspend();
        }
        else
        {
            _threadTime.Resume();
        }
        MessageDispatcher.SendMessage(NotificationName.EventTimeState);
    }

    public void SetSpeed()
    {
        SetSpeed((GameSpeed + 1) % SPEED_MAX);
    }

    public void SetSpeed(int value)
    {
        int speed = CheckSpeed(value);
        if (speed != GameSpeed)
        {
            GameSpeed = speed;
            MessageDispatcher.SendMessage(NotificationName.EventTimeState);
        }

    }

    private int CheckSpeed(int speed)
    {
        if (speed < 1)
        {
            speed = 1;
        }
        else if (speed > SPEED_MAX)
        {
            speed = SPEED_MAX;
        }
        return speed;
    }

    public override void On_Release()
    {
        _threadTime.EventExecute -= EventExecute;
        TaskAsynManager.Instance.FinishTask(_threadTime.TaskId);
        _threadTime = null;
    }




}

