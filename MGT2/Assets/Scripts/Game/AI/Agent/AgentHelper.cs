using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHelper
{
    /// <summary>
    /// 移动
    /// </summary>
    public const string ACTION_UNIT_MOVE = "ACTION_UNIT_MOVE";
    /// <summary>
    /// 巡逻
    /// </summary>
    public const string ACTION_PATROL = "ACTION_PATROL";

    /// <summary>
    /// 普通攻击
    /// </summary>
    public const string ACTION_ATTACK_NORMAL = "ACTION_ATTACK_NORMAL";


    /// <summary>
    /// 位置
    /// </summary>
    public const string AD_POSITION = "AD_POSITION";
    /// <summary>
    /// 实体数据
    /// </summary>
    public const string AD_ENTITY = "AD_ENTITY";
    /// <summary>
    /// 移动状态标记位
    /// </summary>
    public const string AD_MOVE_STATE = "AD_MOVE_STATE";
    /// <summary>
    /// 目标数据
    /// </summary>
    public const string AD_TARGETS = "AD_TARGETS";




    public const string ACTION_MOVE_AUTO = "ACTION_MOVE_AUTO";
    public const string ACTION_MOVE_FINISH = "ACTION_MOVE_FINISH";

    public const string ACTION_DESTROY_ENEMY = "ACTION_DESTROY_ENEMY";



    public static KeyValuePair<string, object> CreateKeyValue(string key, AgentData data)
    {
        return CreateKeyValue(key, data.Contain(key));
    }

    public static KeyValuePair<string, object> CreateKeyValue(string type, object value)
    {
        //Log.Info($" Create key Value {type}  {value} ");
        return new KeyValuePair<string, object>(type, value);
    }

    public static AgentDataBase CreateData(string strType, bool value)
    {
        return CreateData<AgentDataBase>(strType, value);
    }
    public static T CreateData<T>(string type, bool value) where T : AgentDataBase, new()
    {
        T data = new T();
        data.Initial(type, value);
        return data;
    }


    public static AgentDataEntity CreateData(EntityAssembly entity)
    {
        AgentDataEntity data = CreateData<AgentDataEntity>(AD_ENTITY, false);
        data.SetEntity(entity);
        return data;
    }
    public static AgentDataPosition CreateData(Vector3 value)
    {
        AgentDataPosition data = CreateData<AgentDataPosition>(AD_POSITION, false);
        data.SetValue(value);
        return data;
    }
}
