using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineNotification
{
    /// <summary>
    /// 资源加载完成
    /// </summary>
    public const string RESOURCES_LOAD_FINISH = "resource_load_finish";
    /// <summary>
    /// 地图加载完成
    /// </summary>
    public const string MAP_LOAD_FINISH = "map_load_finish";

    /// <summary>
    /// 地图物体 增加
    /// </summary>
    public const string MAP_ENTITY_ADD = "map_entity_add";
    /// <summary>
    /// 地图物体 移除
    /// </summary>
    public const string MAP_ENTITY_REM = "map_entity_rem";
    /// <summary>
    /// 头顶UI信息添加
    /// </summary>
    public const string ENTITY_UIHEAD_ADD = "entity_uiHead_add";
    /// <summary>
    /// 头顶UI信息移除
    /// </summary>
    public const string ENTITY_UIHEAD_REM = "entity_uiHead_rem";



    /// <summary>
    /// 添加实体组件
    /// </summary>
    public const string ASSEMBLY_ADD = "addition_assembly";

    /// <summary>
    /// 移除实体组件
    /// </summary>
    public const string ASSEMBLY_REM = "remove_assembly";

}
