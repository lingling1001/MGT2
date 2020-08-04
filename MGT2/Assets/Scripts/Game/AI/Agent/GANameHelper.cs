using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GANameHelper
{
    /// <summary>
    /// 角色攻击
    /// </summary>
    public const string ROLE_ATTACK = "role_attack";
    /// <summary>
    /// 巡逻
    /// </summary>
    public const string ROLE_PATROL = "role_patrol";



    public const string ENTITY_DES = "entity_destroy";


    private static Dictionary<int, string> _mapStrType = new Dictionary<int, string>();
    public static string GetName(EnumGADType type)
    {
        int key = (int)type;
        if (!_mapStrType.ContainsKey(key))
        {
            _mapStrType.Add(key, type.ToString());
        }
        return _mapStrType[key];
    }

    public static KeyValuePair<string, object> CreateKeyValue(string type, object value)
    {
        return new KeyValuePair<string, object>(type, value);
    }
}

/// <summary>
/// ActionData 类型
/// </summary>
public enum EnumGADType : int
{
    /// <summary>
    /// 巡逻状态
    /// </summary>
    Patrol,
    /// <summary>
    /// 移动位置
    /// </summary>
    RoleMove,

    /// <summary>
    /// 角色信息
    /// </summary>
    SelfEntity,
    /// <summary>
    /// 搜寻敌人
    /// </summary>
    SearchEnemy,
}