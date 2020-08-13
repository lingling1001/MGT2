using UnityEngine;
/// <summary>
/// 朝向目标点
/// </summary>
public class AEFaceToTarget : AEffectEventBase
{
    public override void Execute()
    {
        base.Execute();
        SetIsFinish(true);
        if (Target == null)
        {
            return;
        }
        Vector3 dir = (Target.Position - Owner.Position).normalized;
        Owner.AssyDirection.SetValue(dir);
    }


}

