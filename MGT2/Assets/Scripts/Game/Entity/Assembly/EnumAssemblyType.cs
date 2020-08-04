public enum EnumAssemblyType : int
{
    Entity = 1,
    /// <summary>
    /// 显示
    /// </summary>
    View = 2,
    /// <summary>
    /// 位置
    /// </summary>
    Position = 3,
    /// <summary>
    /// 名字
    /// </summary>
    ViewName = 4,
    /// <summary>
    /// 朝向
    /// </summary>
    Direction = 5,
    /// <summary>
    /// 属性
    /// </summary>
    Attribute = 6,
    /// <summary>
    /// 动画。
    /// </summary>
    Animator = 7,
    /// <summary>
    /// 碰撞
    /// </summary>
    Collider = 8,
    /// <summary>
    /// 加载完成
    /// </summary>
    LoadFinish = 9,
    /// <summary>
    /// 阵营
    /// </summary>
    Camp = 10,
    /// <summary>
    /// 技能
    /// </summary>
    Ability = 11,
    ///// <summary>
    ///// 移动到坐标点
    ///// </summary>
    //MoveToPosition = 12,
    /// <summary>
    /// 超向目标点
    /// </summary>
    MoveToDirection = 13,
    /// <summary>
    /// 大小
    /// </summary>
    ViewScale = 14,
    /// <summary>
    /// 角色数据表
    /// </summary>
    PrototypeRole = 15,
    /// <summary>
    /// 角色控制器
    /// </summary>
    AniController = 16,
    /// <summary>
    /// 武器信息
    /// </summary>
    Weapon = 17,
    /// <summary>
    /// AI
    /// </summary>
    GoapAgent = 18,
    /// <summary>
    /// AI目标
    /// </summary>
    GoapPlan = 19,
    /// <summary>
    /// 搜寻敌人
    /// </summary>
    EyeSensor = 20,
    /// <summary>
    /// 角色移动
    /// </summary>
    RoleMove,
    /// <summary>
    /// 角色
    /// </summary>
    Role,
    /// <summary>
    /// 死亡
    /// </summary>
    EntityDead,
    /// <summary>
    ///人物上方信息
    /// </summary>
    HeadUIItem,
    /// <summary>
    /// 角色头顶版
    /// </summary>
    TransHead,
    /// <summary>
    /// 控制的角色
    /// </summary>
    RoleControl,
    /// <summary>
    /// 技能释放
    /// </summary>
    AbilityCast,
}
