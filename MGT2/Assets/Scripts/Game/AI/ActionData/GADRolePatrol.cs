using UnityEngine;

public class GADRolePatrol : GADEntity
{
    public int IdleTime;
    public Vector3 MovePos;

    public void SetIdleTime(int value)
    {
        IdleTime = value;
    }
    public void SetMovePos(Vector3 value)
    {
        MovePos = value;
    }
}