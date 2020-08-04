using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHelper
{
    public static AssemblyRole GetNearOtherCamp(AssemblyRole entityTarget, int type, float distance)
    {
        List<AssemblyRole> list = GetOthersByCamp(entityTarget, type);
        return GetNearOtherCamp(entityTarget, list, distance);
    }
    public static List<AssemblyRole> GetOthersByCamp(AssemblyRole target, int type)
    {
        return GetOthersByCamp(target, new List<int>() { type });
    }

    public static List<AssemblyRole> GetOthersByCamp(AssemblyRole target, List<int> types)
    {
        List<AssemblyRole> list = new List<AssemblyRole>();
        List<AssemblyRole> map = GetAllRoles();
        foreach (var item in map)
        {
            if (!types.Contains(item.AssyEntity.EntityType))
            {
                continue;
            }
            if (item.Owner.ContainsKey(EnumAssemblyType.EntityDead))
            {
                continue;
            }
            if (item.AssyEntity.EntityId == target.AssyEntity.EntityId)//相同ID
            {
                continue;
            }
            if (!item.Owner.ContainsKey(EnumAssemblyType.Camp))//不包含阵营
            {
                continue;
            }
            if (item.AssyCamp.Id == target.AssyCamp.Id)//阵营相同
            {
                continue;
            }
            list.Add(item);
        }
        return list;
    }



    public static AssemblyRole GetNearOtherCamp(AssemblyRole target, List<AssemblyRole> list, float distance)
    {
        AssemblyRole entity = null;
        float minDis = 0;
        //AssemblyRole targetRole = target.GetData<AssemblyRole>(EnumAssemblyType.Role);

        foreach (var item in list)
        {
            //AssemblyRole itemRole = item.GetData<AssemblyRole>(EnumAssemblyType.Role);
            float dis = Vector3.Distance(target.AssyPosition.Position, item.AssyPosition.Position);
            if (dis > distance)
            {
                continue;
            }
            if (entity == null)
            {
                minDis = dis;
                entity = item;
                continue;
            }
            if (dis < minDis)
            {
                entity = item;
                minDis = dis;
            }
        }
        return entity;
    }

    private static List<AssemblyRole> _listTemp = new List<AssemblyRole>();
    public static List<AssemblyRole> GetAllRoles()
    {
        _listTemp.Clear();
        foreach (var item in MapEntityManager.Instance.MapEntities)
        {
            if (item.Value.ContainsKey(EnumAssemblyType.Role))
            {
                _listTemp.Add(item.Value.GetData<AssemblyRole>(EnumAssemblyType.Role));
            }
        }
        return _listTemp;
    }
    /// <summary>
    /// 是否可以有多个组件
    /// </summary>
    public static bool AssemblyIsMultipe(EnumAssemblyType assemblyType)
    {
        return assemblyType == EnumAssemblyType.Ability;
    }
}
