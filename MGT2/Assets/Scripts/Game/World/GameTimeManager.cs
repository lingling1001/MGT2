using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : ManagerBase
{
    public const int SPEED_DEFAULT = 1000;
    public int GameSpeed { get; private set; }
    public long RunTime { get; private set; }

    private TaskAsyncThreadFrame _threadTime;

    private List<int> _listSpeed = new List<int> { 1, 2, 5, };

    public override void On_Init()
    {
        int taskId = TaskAsynManager.Instance.GetFreeTaskId();
        _threadTime = new TaskAsyncThreadFrame(taskId, SPEED_DEFAULT);
        SetSpeed(1);
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
        int index = _listSpeed.FindIndex(item => item == GameSpeed);
        index = index < 0 ? 0 : index;
        int nextIndex = index + 1;
        if (nextIndex >= _listSpeed.Count)
        {
            nextIndex = 0;
        }
        SetSpeed(_listSpeed[nextIndex]);
    }


    public void SetSpeed(int value)
    {
        if (value != GameSpeed)
        {
            GameSpeed = value;
            _threadTime.SetIntervalMs(SPEED_DEFAULT / GameSpeed);
            MessageDispatcher.SendMessage(NotificationName.EventGameSpeed);
        }
    }



    public override void On_Release()
    {
        _threadTime.EventExecute -= EventExecute;
        TaskAsynManager.Instance.FinishTask(_threadTime.TaskId);
        _threadTime = null;
    }




}

