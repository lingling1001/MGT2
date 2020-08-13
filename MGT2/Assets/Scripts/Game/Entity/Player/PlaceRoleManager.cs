using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFrameWork;
using System;
/// <summary>
/// 放置管理器
/// </summary>
public class PlaceRoleManager : Singleton<PlaceRoleManager>
{

    public void PutRoleToMap(AssemblyRole assemblyRole)
    {
        if (MapEntityManager.Instance.ContainsKey(assemblyRole.EntityId))
        {
            return;
        }

        //添加View组件 显示到世界中 ,
        FactoryEntity.InitialView(GetRangePos(), assemblyRole);

        FactoryAssembly.AddLoadWeapon(assemblyRole.Owner, 4);

        MapEntityManager.Instance.AddEntity(assemblyRole.Owner);

    }

    public static Vector3 GetRangePos()
    {
        return new Vector3(0, 1, 0);
        return new Vector3(UnityEngine.Random.Range(30, 70), 0, UnityEngine.Random.Range(10, 30));
    }
}
