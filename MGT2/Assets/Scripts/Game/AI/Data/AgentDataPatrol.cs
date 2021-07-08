using System.Collections.Generic;
using UnityEngine;

public class AgentDataPatrol : AgentDataBase
{
    /// <summary>
    /// 巡逻位置点
    /// </summary>
    public Vector3 PatrolIndex { get; private set; }

    public void SetPatrol(Vector3 patrolIndex)
    {
        PatrolIndex = patrolIndex;
    }



}
