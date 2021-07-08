using System.Collections.Generic;
using UnityEngine;

public partial class EntityHelper
{
    public static AssemblyCache GetNearOtherCamp(AssemblyCache entityTarget, float distance)
    {
        List<AssemblyCache> list = GetOthersByCamp(entityTarget);
        return GetNearEntity(entityTarget, list, distance);
    }
    public static AssemblyCache GetNearEntity(AssemblyCache target, List<AssemblyCache> list, float distance)
    {
        AssemblyCache entity = null;
        float minDis = 0;
        foreach (var item in list)
        {
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

    public static List<AssemblyCache> GetNearOthersByCamp(AssemblyCache target, float distance)
    {
        List<AssemblyCache> res = new List<AssemblyCache>();
        List<AssemblyCache> list = GetOthersByCamp(target);
        foreach (var item in list)
        {
            float dis = Vector3.Distance(target.AssyPosition.Position, item.AssyPosition.Position);
            if (dis > distance)
            {
                continue;
            }
            res.Add(item);
        }
        return res;
    }

    /// <summary>
    /// 获取其他阵营数据
    /// </summary>
    public static List<AssemblyCache> GetOthersByCamp(AssemblyCache target)
    {
        List<AssemblyCache> list = new List<AssemblyCache>();
        List<AssemblyCache> map = GetAllRoles();
        foreach (var item in map)
        {
            if (item.Owner.EntityId == target.Owner.EntityId)//相同ID
            {
                continue;
            }

            list.Add(item);
        }
        return list;
    }

    private static List<AssemblyCache> _listTemp = new List<AssemblyCache>();
    public static List<AssemblyCache> GetAllRoles()
    {
        _listTemp.Clear();
        Dictionary<int, EntityAssembly> map = GameManager<EntityManager>.QGetOrAddMgr().GetAllDatas();
        foreach (var item in map)
        {
            AssemblyCache assembly = item.Value.GetData<AssemblyCache>();
            if (assembly != null)
            {
                _listTemp.Add(assembly);
            }
        }
        return _listTemp;
    }
}
