using UnityEngine;
public class AssemblyRole : AssemblyBase
{
    public Vector3 Position
    {
        get
        {
            if (AssyPosition != null)
            {
                return AssyPosition.Position;
            }
            return Vector3.zero;
        }
    }

    public int EntityId
    {
        get
        {
            if (AssyEntity != null)
            {
                return AssyEntity.EntityId;
            }
            return -1;
        }
    }
    public int EntityType
    {
        get
        {
            if (AssyEntity != null)
            {
                return AssyEntity.EntityType;
            }
            return -1;
        }
    }

    /// <summary>
    /// ID 常用组件 缓存
    /// </summary>
    public AssemblyEntity AssyEntity
    {
        get
        {
            if (assyEntity == null)
            {
                assyEntity = Owner.GetData<AssemblyEntity>(EnumAssemblyType.Entity);
            }
            return assyEntity;
        }
    }
    private AssemblyEntity assyEntity;

    /// <summary>
    /// 位置。 常用组件 缓存
    /// </summary>
    public AssemblyPosition AssyPosition
    {
        get
        {
            if (assyPosition == null)
            {
                assyPosition = Owner.GetData<AssemblyPosition>(EnumAssemblyType.Position);
            }
            return assyPosition;
        }
    }
    private AssemblyPosition assyPosition;


    /// <summary>
    /// 朝向
    /// </summary>
    public AssemblyDirection AssyDirection
    {
        get
        {
            if (assyDirection == null)
            {
                assyDirection = Owner.GetData<AssemblyDirection>(EnumAssemblyType.Direction);
            }
            return assyDirection;
        }
    }
    private AssemblyDirection assyDirection;


    /// <summary>
    /// 能力信息
    /// </summary>
    public AssemblyAbility AssyAbility
    {
        get
        {
            if (assyAbility == null)
            {
                assyAbility = Owner.GetData<AssemblyAbility>(EnumAssemblyType.Ability);
            }
            return assyAbility;
        }
    }
    private AssemblyAbility assyAbility;


    /// <summary>
    /// 数据类
    /// </summary>
    public AssemblyPrototypeRole AssyPrototypeRole
    {
        get
        {
            if (assyPrototypeRole == null)
            {
                assyPrototypeRole = Owner.GetData<AssemblyPrototypeRole>(EnumAssemblyType.PrototypeRole);
            }
            return assyPrototypeRole;
        }
    }
    private AssemblyPrototypeRole assyPrototypeRole;

    /// <summary>
    /// 属性
    /// </summary>
    public AssemblyAttribute AssyAttribute
    {
        get
        {
            if (assyAttribute == null)
            {
                assyAttribute = Owner.GetData<AssemblyAttribute>(EnumAssemblyType.Attribute);
            }
            return assyAttribute;
        }
    }
    private AssemblyAttribute assyAttribute;
    /// <summary>
    /// AI
    /// </summary>
    public AssemblyGoapAgent AssyGoapAgent
    {
        get
        {
            if (assyGoapAgent == null)
            {
                assyGoapAgent = Owner.GetData<AssemblyGoapAgent>(EnumAssemblyType.GoapAgent);
            }
            return assyGoapAgent;
        }
    }
    public AssemblyGoapAgent assyGoapAgent;
    /// <summary>
    /// 动画组件
    /// </summary>
    public AssemblyAnimator AssyAnimator
    {
        get
        {
            if (assyAnimator == null)
            {
                assyAnimator = Owner.GetData<AssemblyAnimator>(EnumAssemblyType.Animator);
            }
            return assyAnimator;
        }
    }
    private AssemblyAnimator assyAnimator;

    /// <summary>
    /// AI寻找敌人组件
    /// </summary>
    public AssemblyEyeSensor AssyEyeSensor
    {
        get
        {
            if (assyEyeSensor == null && Owner.ContainsKey(EnumAssemblyType.EyeSensor))
            {
                assyEyeSensor = Owner.GetData<AssemblyEyeSensor>(EnumAssemblyType.EyeSensor);
            }
            return assyEyeSensor;
        }
    }

    private AssemblyEyeSensor assyEyeSensor;

    /// <summary>
    /// 移动组件
    /// </summary>
    public AssemblyRoleMove AssyRoleMove
    {
        get
        {
            if (assyRoleMove == null)
            {
                assyRoleMove = Owner.GetData<AssemblyRoleMove>(EnumAssemblyType.RoleMove);
            }
            return assyRoleMove;
        }
    }
    private AssemblyRoleMove assyRoleMove;
    /// <summary>
    /// 阵营
    /// </summary>
    public AssemblyCamp AssyCamp
    {
        get
        {
            if (assyCamp == null)
            {
                assyCamp = Owner.GetData<AssemblyCamp>(EnumAssemblyType.Camp);
            }
            return assyCamp;
        }
    }

    private AssemblyCamp assyCamp;

    /// <summary>
    /// 死亡组件
    /// </summary>
    public AssemblyEntityDead AssyEntityDead
    {
        get
        {
            if (assyEntityDead == null)
            {
                assyEntityDead = Owner.GetData<AssemblyEntityDead>(EnumAssemblyType.EntityDead);
            }
            return assyEntityDead;
        }
    }
    private AssemblyEntityDead assyEntityDead;

    /// <summary>
    /// 朝向移动组件
    /// </summary>
    public AssemblyMoveToDirection AssyMoveToDirection
    {
        get
        {
            if (assyMoveToDirection == null)
            {
                assyMoveToDirection = Owner.GetData<AssemblyMoveToDirection>(EnumAssemblyType.MoveToDirection);
            }
            return assyMoveToDirection;
        }
    }
    private AssemblyMoveToDirection assyMoveToDirection;


    public AssemblyView AssemblyView
    {
        get
        {
            if (assemblyView == null)
            {
                assemblyView = Owner.GetData<AssemblyView>(EnumAssemblyType.View);
            }
            return assemblyView;
        }
    }
    private AssemblyView assemblyView;
    public override void OnRelease()
    {
        ClearCache();
        base.OnRelease();
    }

    public void ClearCache()
    {
        assyEntity = null;
        assyPosition = null;
        assyDirection = null;
        assyEyeSensor = null;
        assyAbility = null;
        assyPrototypeRole = null;
        assyAnimator = null;
        assyAttribute = null;
        assyRoleMove = null;
        assyCamp = null;
        assyGoapAgent = null;
        assyEntityDead = null;
    }

    public override string ToString()
    {
        if (AssyPrototypeRole != null)
        {
            return string.Format(" ({0} # [{1}] ) ", EntityId, AssyPrototypeRole.Value.Name);
        }
        return base.ToString();
    }
}