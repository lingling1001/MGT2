using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色管理
/// </summary>
public class RoleManager : Singleton<RoleManager>
{
    /// <summary>
    /// 当前拥有的角色
    /// </summary>
    private List<AssemblyRole> _listPlayers = new List<AssemblyRole>();
    public List<AssemblyRole> ListPlayers { get { return _listPlayers; } }

    private AssemblyRole _curControlRole;
    public AssemblyRole CurControlRole { get { return _curControlRole; } }

    public void OnInit()
    {
        //_listPlayers.AddRange(LoadDefaultPlayer());
        //for (int cnt = 0; cnt < _listPlayers.Count; cnt++)
        //{
        //    AssemblyRole role = _listPlayers[cnt];
        //    AssyEntityManager.Instance.Addition(role.EntityId, role.Owner);
        //}
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    public AssemblyRole CreateAssemblyRole(PrototypeRole data)
    {
        int entityId = AssyEntityManager.Instance.GetFreeEntityKey();
        AssemblyEntityBase entity = FactoryEntity.CreateEntity(entityId);
        FactoryEntity.InitialData(DefineCamp.PLAYER, data, data.GetListAttribute(), entity);
        AssemblyRole assemblyRole = entity.GetData<AssemblyRole>(EnumAssemblyType.Role);
        AddRoleToList(assemblyRole);

        return assemblyRole;
    }

    /// <summary>
    /// 当前使用的角色
    /// </summary>
    public void SelectRole(AssemblyRole assemblyRole)
    {
        _curControlRole = assemblyRole;
        FactoryAssembly.AddRoleControl(assemblyRole.Owner);
        FactoryAssembly.AddMoveToDirection(assemblyRole.Owner);
    }

    /// <summary>
    /// 初始化角色到地图中
    /// </summary>
    public void InitRoleToMap()
    {
        for (int cnt = 0; cnt < ListPlayers.Count; cnt++)
        {
            if (ListPlayers[cnt].Owner.ContainsKey(EnumAssemblyType.RoleControl))
            {
                PlaceRoleManager.Instance.PutRoleToMap(ListPlayers[cnt]);
            }
        }
    }

    /// <summary>
    /// 添加角色数据
    /// </summary>
    public void AddRoleToList(AssemblyRole role)
    {
        _listPlayers.Add(role);
    }




    public void OnRelease()
    {
        for (int cnt = 0; cnt < ListPlayers.Count; cnt++)
        {
            AssyEntityManager.Instance.Remove(ListPlayers[cnt].EntityId);
        }
        _listPlayers.Clear();

    }

}
public class DefineSaveName
{
    public const string PLAYER_INFO = "Playerinfo";

    //ES3.Save<Vector3>(DefineSaveName.PLAYER_POSITION, pos);
}
///// <summary>
///// 加载默认角色
///// </summary>
//private List<AssemblyRole> LoadDefaultPlayer()
//{
//    List<AssemblyRole> list = new List<AssemblyRole>();
//    int[] arrs = PrototypeGameConfig.GetConfigList<int>(EnumGameConfig.DefaultRole);
//    for (int cnt = 0; cnt < arrs.Length; cnt++)
//    {
//        PrototypeRole roleData = PrototypeManager<PrototypeRole>.Instance.GetPrototype(arrs[cnt]);
//        if (roleData == null)
//        {
//            continue;
//        }
//        int entityId = AssyEntityManager.Instance.GetFreeEntityKey();
//        AssemblyEntityBase entity = FactoryEntity.CreateEntity(entityId);
//        FactoryEntity.InitialData(DefineCamp.PLAYER, roleData, roleData.GetListAttribute(), entity);
//        AssemblyRole assemblyRole = entity.GetData<AssemblyRole>(EnumAssemblyType.Role);

//        list.Add(assemblyRole);
//    }
//    return list;
//}



//public string Vector3ToString(Vector3 pos)
//{
//    return pos.x + "," + pos.y + "," + pos.z;
//}