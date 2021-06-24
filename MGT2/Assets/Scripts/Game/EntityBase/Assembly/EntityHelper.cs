using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EntityHelper
{

    //public static AssemblyRole GetNearOtherCamp(AssemblyRole entityTarget, int type, float distance)
    //{
    //    List<AssemblyRole> list = GetOthersByCamp(entityTarget, type);
    //    return GetNearOtherCamp(entityTarget, list, distance);
    //}
    //public static List<AssemblyRole> GetOthersByCamp(AssemblyRole target, int type)
    //{
    //    return GetOthersByCamp(target, new List<int>() { type });
    //}








    /// <summary>
    /// 是否可以有多个组件
    /// </summary>
    public static bool AssemblyIsMultipe<T>(T data)
    {
        return false;
    }



    ///// <summary>
    ///// 是否在矩内
    ///// </summary>
    //public static bool InRectangle(VInt3 min, VInt3 max, VInt3 point)
    //{
    //    if (point.x < min.x)
    //    {
    //        return false;
    //    }
    //    if (point.x > max.x)
    //    {
    //        return false;
    //    }
    //    if (point.z < min.z)
    //    {
    //        return false;
    //    }
    //    if (point.z > max.z)
    //    {
    //        return false;
    //    }
    //    return true;
    //}

   
}
