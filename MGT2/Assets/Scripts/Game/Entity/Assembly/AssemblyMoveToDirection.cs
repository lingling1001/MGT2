using MFrameWork;
using UnityEngine;

public class AssemblyMoveToDirection : AssemblySelfRole, IUpdate
{
    public bool IsMove;
    public int Priority => DefinePriority.NORMAL;

    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        RegisterInterfaceManager.RegisteUpdate(this);
    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (IsMove)
        {
            Vector3 targetDir = SelfEntity.Position + SelfEntity.AssyDirection.Value;
            float step = SelfEntity.AssyAttribute.GetValue(DefineAttributeId.SPEED_MOVE) * Time.deltaTime;
            Vector3 targetPos = Vector3.MoveTowards(SelfEntity.Position, targetDir, step);
            if (!FindPathManager.Instance.IsCanWalk(targetPos))
            {
                return;
            }
            SelfEntity.AssyPosition.SetPosition(targetPos);
        }
    }
    public void SetMove(bool isMove)
    {
        IsMove = isMove;
        if (isMove)
        {
            SelfEntity.AssyAnimator.SetValue(EnumAnimator.Run);
        }
        else
        {
            SelfEntity.AssyAnimator.SetValue(EnumAnimator.Idle);
        }
    }

}
