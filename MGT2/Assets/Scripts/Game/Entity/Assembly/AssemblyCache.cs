using Newtonsoft.Json;
using UnityEngine;

public class AssemblyCache : AssemblyBase
{
    private AssemblyPosition assyPosition;
    private AssemblyAttribute assyAttribute;
    private AssemblyView assemblyView;
    private AssemblyCameraFollow assyCameraFollow;
    private AssemblyRoleInfo assyRoleInfo;
    private AssemblyRoleControl assyRoleControl;
    private AssemblyRoleAction assyRoleAction;



    [Newtonsoft.Json.JsonIgnore]
    public AssemblyRoleInfo AssyRoleInfo
    {
        get
        {
            if (assyRoleInfo == null)
            {
                assyRoleInfo = Owner.GetData<AssemblyRoleInfo>();
            }
            return assyRoleInfo;
        }
    }

    [Newtonsoft.Json.JsonIgnore]
    public AssemblyRoleControl AssyRoleControl
    {
        get
        {
            if (assyRoleControl == null)
            {
                assyRoleControl = Owner.GetData<AssemblyRoleControl>();
            }
            return assyRoleControl;
        }
    }

    [Newtonsoft.Json.JsonIgnore]
    public AssemblyRoleAction AssyRoleAction
    {
        get
        {
            if (assyRoleAction == null)
            {
                assyRoleAction = Owner.GetData<AssemblyRoleAction>();
            }
            return assyRoleAction;
        }
    }
    private AssemblyDirection assyDirection;
    private AssemblyGoapAgent assyGoapAgent;
    private AssemblyAnimator assyAnimator;
    private AssemblyEyeSensor assyEyeSensor;
    //private AssemblyCamp assyCamp;
    //private AssemblyPrototypeRole assyPrototypeRole;
    private AssemblyAutoMove assemblyAutoMove;
    //private AssemblyCastle assemblyCastle;
    //private AssemblyUnitCastle assemblyUnitCastle;
    //private AssemblyJoystick assemblyJoystick;
    //private AssemblyMoveToDirection assemblyMoveToDirection;
    [Newtonsoft.Json.JsonIgnore]
    public AssemblyCameraFollow AassyCameraFollow
    {
        get
        {
            if (assyCameraFollow == null)
            {
                assyCameraFollow = Owner.GetData<AssemblyCameraFollow>();
            }
            return assyCameraFollow;
        }
    }

    //public AssemblyMoveToDirection AssemblyMoveToDirection
    //{
    //    get
    //    {
    //        if (assemblyMoveToDirection == null)
    //        {
    //            assemblyMoveToDirection = Owner.GetData<AssemblyMoveToDirection>();
    //        }
    //        return assemblyMoveToDirection;
    //    }
    //}
    /// <summary>
    /// 遥感组件
    /// </summary>
    //public AssemblyJoystick AssemblyJoystick
    //{
    //    get
    //    {
    //        if (assemblyJoystick == null)
    //        {
    //            assemblyJoystick = Owner.GetData<AssemblyJoystick>();
    //        }
    //        return assemblyJoystick;
    //    }
    //}

    //private AssemblyTGSCell assemblyTGSCell;

    //public AssemblyTGSCell AssemblyTGSCell
    //{
    //    get
    //    {
    //        if (assemblyTGSCell == null)
    //        {
    //            assemblyTGSCell = Owner.GetData<AssemblyTGSCell>();
    //        }
    //        return assemblyTGSCell;
    //    }
    //}
    /// <summary>
    /// 移动组件
    /// </summary>
    public AssemblyAutoMove AssemblyAutoMove
    {
        get
        {
            if (assemblyAutoMove == null)
            {
                assemblyAutoMove = Owner.GetData<AssemblyAutoMove>();
            }
            return assemblyAutoMove;
        }
    }
    /// <summary>
    /// 城堡单位组件
    /// </summary>
    //public AssemblyUnitCastle AssemblyUnitCastle
    //{
    //    get
    //    {
    //        if (assemblyUnitCastle == null)
    //        {
    //            assemblyUnitCastle = Owner.GetData<AssemblyUnitCastle>();
    //        }
    //        return assemblyUnitCastle;
    //    }
    //}
    /// <summary>
    /// 城池组件
    /// </summary>
    //public AssemblyCastle AssemblyCastle
    //{
    //    get
    //    {
    //        if (assemblyCastle == null)
    //        {
    //            assemblyCastle = Owner.GetData<AssemblyCastle>();
    //        }
    //        return assemblyCastle;
    //    }
    //}
    [Newtonsoft.Json.JsonIgnore]
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

    /// <summary>
    /// 位置。 常用组件 缓存
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public AssemblyPosition AssyPosition
    {
        get
        {
            if (assyPosition == null)
            {
                assyPosition = Owner.GetData<AssemblyPosition>();
            }
            return assyPosition;
        }
    }


    /// <summary>
    /// 朝向
    /// </summary>
    public AssemblyDirection AssyDirection
    {
        get
        {
            if (assyDirection == null)
            {
                assyDirection = Owner.GetData<AssemblyDirection>();
            }
            return assyDirection;
        }
    }





    /// <summary>
    /// 数据类
    /// </summary>
    //public AssemblyPrototypeRole AssyPrototypeRole
    //{
    //    get
    //    {
    //        if (assyPrototypeRole == null)
    //        {
    //            assyPrototypeRole = Owner.GetData<AssemblyPrototypeRole>();
    //        }
    //        return assyPrototypeRole;
    //    }
    //}

    /// <summary>
    /// 属性
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public AssemblyAttribute AssyAttribute
    {
        get
        {
            if (assyAttribute == null)
            {
                assyAttribute = Owner.GetData<AssemblyAttribute>();
            }
            return assyAttribute;
        }
    }

    /// <summary>
    /// AI
    /// </summary>
    public AssemblyGoapAgent AssyGoapAgent
    {
        get
        {
            if (assyGoapAgent == null)
            {
                assyGoapAgent = Owner.GetData<AssemblyGoapAgent>();
            }
            return assyGoapAgent;
        }
    }

    /// <summary>
    /// 动画组件
    /// </summary>
    public AssemblyAnimator AssyAnimator
    {
        get
        {
            if (assyAnimator == null)
            {
                assyAnimator = Owner.GetData<AssemblyAnimator>();
            }
            return assyAnimator;
        }
    }


    /// <summary>
    /// AI寻找敌人组件
    /// </summary>
    public AssemblyEyeSensor AssyEyeSensor
    {
        get
        {
            if (assyEyeSensor == null && Owner.ContainsKey(typeof(AssemblyEyeSensor)))
            {
                assyEyeSensor = Owner.GetData<AssemblyEyeSensor>();
            }
            return assyEyeSensor;
        }
    }



    ///// <summary>
    ///// 阵营
    ///// </summary>
    //public AssemblyCamp AssyCamp
    //{
    //    get
    //    {
    //        if (assyCamp == null)
    //        {
    //            assyCamp = Owner.GetData<AssemblyCamp>();
    //        }
    //        return assyCamp;
    //    }
    //}
    [Newtonsoft.Json.JsonIgnore]
    public AssemblyView AssemblyView
    {
        get
        {
            if (assemblyView == null)
            {
                assemblyView = Owner.GetData<AssemblyView>();
            }
            return assemblyView;
        }
    }

    ///// <summary>
    ///// 死亡组件
    ///// </summary>
    //public AssemblyEntityDead AssyEntityDead
    //{
    //    get
    //    {
    //        if (assyEntityDead == null)
    //        {
    //            assyEntityDead = Owner.GetData<AssemblyEntityDead>(EnumAssemblyType.EntityDead);
    //        }
    //        return assyEntityDead;
    //    }
    //}
    //private AssemblyEntityDead assyEntityDead;






    protected override void OnRelease()
    {
        assyPosition = null;
        assyAttribute = null;
        assyRoleControl = null;
        assyDirection = null;
        assyEyeSensor = null;
        assyAnimator = null;
        assyGoapAgent = null;
        base.OnRelease();
    }

}