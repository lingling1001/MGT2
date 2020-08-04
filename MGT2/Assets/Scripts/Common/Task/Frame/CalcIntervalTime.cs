using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class CalcIntervalTime
{

    private long _startTime;
    private long _stopTime;
    public void Start(long startTime, long intervalTicks)
    {

        _startTime = startTime;
        _stopTime = _startTime + intervalTicks;

    }

    public long ExecuteSubValue(long curTime)
    {
        return _stopTime - curTime;
    }
   

}