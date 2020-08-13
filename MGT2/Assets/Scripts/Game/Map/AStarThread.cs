using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarThread : TaskAsyncThreadFrame
{
    public long Frame;
    public long Ticks;
    public AStarThread(int taskId, int intervalMs) : base(taskId, intervalMs)
    {

    }
    protected override void OnExecute(long frameCount, long ticks)
    {
        Frame = frameCount;
        Ticks = ticks;
    }
}
