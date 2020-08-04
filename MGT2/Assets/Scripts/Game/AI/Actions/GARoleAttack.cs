public class GARoleAttack : GoapAction
{
    private AssemblyRole _selfEntity;
    private AssemblyRoleMove _roleMove;
    private GADRolePatrol _dataPatrol;
    private bool _isInit;
    public override bool CheckProceduralPrecondition(object agent)
    {
        // Log.Debug("   CheckProceduralPrecondition  GARoleAttack");
        GAData data = agent as GAData;
        _dataPatrol = data.GetData<GADRolePatrol>(EnumGADType.Patrol);
        _selfEntity = data.GetData<GADEntity>(EnumGADType.SelfEntity).Entity as AssemblyRole;
        if (_selfEntity.AssyEyeSensor.CheckTargetIsNull())
        {
            return false;
        }
        target = agent;
        return true;
    }

    public override bool perform(object agent)
    {
        //Log.Debug("   CheckProceduralPrecondition  perform");
        if (_selfEntity.AssyEyeSensor.CheckTargetIsNull())
        {
            SetIsDone(true);
            return true;
        }
        SkillManager.AttackNormal(_selfEntity, _selfEntity.AssyEyeSensor.Target);
        return true;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override void reset()
    {
        // Log.Debug("   CheckProceduralPrecondition  reset");

    }
}
