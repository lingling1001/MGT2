using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MFrameWork;

public class TaskAsynManager : Singleton<TaskAsynManager>
{
    private Dictionary<int, ITaskAsyncable> mapTaskAsync = new Dictionary<int, ITaskAsyncable>();

    public void AdditionTask(ITaskAsyncable task)
    {
        mapTaskAsync.Add(task.TaskId, task);
        task.Start();
    }


    public bool ContainTask(int id)
    {
        return mapTaskAsync.ContainsKey(id);
    }

    public ITaskAsyncable GetTaskAsyn(int id)
    {
        if (ContainTask(id))
        {
            return mapTaskAsync[id];
        }
        return null;
    }

    public T GetTaskAsyn<T>(int id) where T : class, ITaskAsyncable
    {
        if (ContainTask(id))
        {
            return mapTaskAsync[id] as T;
        }
        return null;
    }

    public void FinishTask(int id)
    {
        if (ContainTask(id))
        {
            ITaskAsyncable thread = mapTaskAsync[id];
            thread.Stop();
            RemoveTask(id);
        }
    }

    public int GetFreeTaskId()
    {
        int taskId = 1;
        while (mapTaskAsync.ContainsKey(taskId))
        {
            taskId++;
        }
        return taskId;
    }
    private void RemoveTask(int id)
    {
        if (ContainTask(id))
        {
            mapTaskAsync.Remove(id);
        }
    }

    public void OnRelease()
    {
        List<int> list = new List<int>(mapTaskAsync.Keys);
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            FinishTask(list[cnt]);
        }
    }


}
