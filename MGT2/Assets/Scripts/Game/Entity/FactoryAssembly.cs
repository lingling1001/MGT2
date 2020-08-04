using com.ootii.Messages;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FactoryAssembly
{
    public static AssemblyAbilityCast AddAbilityCast(AssemblyEntityBase owner)
    {
        AssemblyAbilityCast data = CreateAssembly<AssemblyAbilityCast>(EnumAssemblyType.AbilityCast, owner);
        AssemblyAdd(data, owner);
        return data;
    }
    public static void AddAbilities(AssemblyEntityBase owner, int[] abilities)
    {
        if (abilities == null || abilities.Length == 0)
        {
            return;
        }
        for (int cnt = 0; cnt < abilities.Length; cnt++)
        {
            AddAbility(owner, abilities[cnt]);
        }

    }
    public static AssemblyAbility AddAbility(AssemblyEntityBase owner, int abilityId)
    {
        PrototypeAbility abData = PrototypeManager<PrototypeAbility>.Instance.GetPrototype(abilityId);
        if (abData == null)
        {
            return null;
        }
        AssemblyAbility ability = CreateAssembly<AssemblyAbility>(EnumAssemblyType.Ability, owner);
        ability.SetData(abData);
        AssemblyAdd(ability, owner);
        return ability;
    }

    public static AssemblyWeapon AddLoadWeapon(AssemblyEntityBase owner, int id)
    {
        AssemblyWeapon data = CreateAssembly<AssemblyWeapon>(EnumAssemblyType.Weapon, owner);
        AssemblyAdd(data, owner);
        data.SetValue(id);
        return data;
    }
    /// <summary>
    /// 朝某个方向移动
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public static AssemblyMoveToDirection AddMoveToDirection(AssemblyEntityBase owner)
    {
        AssemblyMoveToDirection data = CreateAssembly<AssemblyMoveToDirection>(EnumAssemblyType.MoveToDirection, owner);
        AssemblyAdd(data, owner);
        return data;
    }
    /// <summary>
    /// 人物头顶版节点
    /// </summary>
    public static AssemblyTransHead AddTransRoleHead(int modelType, AssemblyEntityBase owner)
    {
        AssemblyTransHead data = CreateAssembly<AssemblyTransHead>(EnumAssemblyType.TransHead, owner);
        data.SetModelType(modelType);
        AssemblyAdd(data, owner);
        return data;
    }
    /// <summary>
    /// 人物头顶版信息
    /// </summary>
    public static AssemblyHeadUIItem AddHeadUIItem(AssemblyEntityBase owner)
    {
        AssemblyHeadUIItem data = CreateAssembly<AssemblyHeadUIItem>(EnumAssemblyType.HeadUIItem, owner);
        AssemblyAdd(data, owner);
        return data;
    }
    public static AssemblyEntityDead AddEntityDead(AssemblyEntityBase owner)
    {
        AssemblyEntityDead data = CreateAssembly<AssemblyEntityDead>(EnumAssemblyType.EntityDead, owner);
        AssemblyAdd(data, owner);
        return data;
    }
    /// <summary>
    /// 人物信息
    /// </summary>
    public static AssemblyRole AddRole(AssemblyEntityBase owner)
    {
        AssemblyRole data = CreateAssembly<AssemblyRole>(EnumAssemblyType.Role, owner);
        AssemblyAdd(data, owner);
        return data;
    }
    /// <summary>
    /// 游戏对象ID
    /// </summary>
    public static AssemblyEntity AddEntityId(int entityId, int type, AssemblyEntityBase owner)
    {
        AssemblyEntity data = CreateAssembly<AssemblyEntity>(EnumAssemblyType.Entity, owner);
        data.SetEntityId(entityId, type);
        AssemblyAdd(data, owner);
        return data;
    }
    /// <summary>
    /// 人物移动
    /// </summary>
    public static AssemblyRoleMove AddRoleMove(AssemblyEntityBase owner)
    {
        AssemblyRoleMove data = CreateAssembly<AssemblyRoleMove>(EnumAssemblyType.RoleMove, owner);
        AssemblyAdd(data, owner);
        return data;
    }

    public static AssemblyRoleControl AddRoleControl(AssemblyEntityBase owner)
    {
        AssemblyRoleControl data = CreateAssembly<AssemblyRoleControl>(EnumAssemblyType.RoleControl, owner);
        AssemblyAdd(data, owner);
        return data;
    }

    /// <summary>
    /// AI检测器
    /// </summary>
    public static AssemblyEyeSensor AddEyeSensor(AssemblyEntityBase owner)
    {
        AssemblyEyeSensor data = CreateAssembly<AssemblyEyeSensor>(EnumAssemblyType.EyeSensor, owner);
        AssemblyAdd(data, owner);
        return data;
    }



    /// <summary>
    /// 添加阵营 
    /// </summary>
    public static void AddCamp(int campId, AssemblyEntityBase owner)
    {
        AssemblyCamp data = CreateAssembly<AssemblyCamp>(EnumAssemblyType.Camp, owner);
        data.SetValue(campId);
        AssemblyAdd(data, owner);
    }
    /// <summary>
    /// 添加属性列表
    /// </summary>
    public static void AddAttribute(List<int[]> arrs, AssemblyEntityBase owner)
    {
        List<AttributeData> list = FactoryAttribute.CreateList(arrs);
        AssemblyAttribute data = CreateAssembly<AssemblyAttribute>(EnumAssemblyType.Attribute, owner);
        data.Initial(list);
        AssemblyAdd(data, owner);
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    public static void AddPrototypeRole(PrototypeRole value, AssemblyEntityBase owner)
    {
        AssemblyPrototypeRole data = CreateAssembly<AssemblyPrototypeRole>(EnumAssemblyType.PrototypeRole, owner);
        data.SetValue(value);
        AssemblyAdd(data, owner);
    }

    //public static void AddAbility(AssemblyEntityBase owner, AssemblyRole roleData)
    //{
    //    AssemblyAbility data = CreateAssembly<AssemblyAbility>(EnumAssemblyType.Ability, owner);
    //    List<AbilityDataNew> list = FactoryAbility.CreateAbilityDatasNew(roleData);
    //    if (list != null)
    //    {
    //        for (int cnt = 0; cnt < list.Count; cnt++)
    //        {
    //            data.AddAbility(list[cnt]);
    //        }
    //    }
    //    AssemblyAdd(data, owner);
    //}

    /// <summary>
    /// 添加AI
    /// </summary>
    public static void AddAgent(GoapAgent value, IGoap goap, AssemblyEntityBase owner)
    {
        AssemblyGoapAgent data = CreateAssembly<AssemblyGoapAgent>(EnumAssemblyType.GoapAgent, owner);
        data.SetValue(value, goap);
        AssemblyAdd(data, owner);
    }

    /// <summary>
    /// 创建 目标和朝向
    /// </summary>
    public static void AddDirection(Vector3 dirtion, AssemblyEntityBase owner)
    {
        AssemblyDirection data = CreateAssembly<AssemblyDirection>(EnumAssemblyType.Direction, owner);
        data.SetValue(dirtion);
        AssemblyAdd(data, owner);

    }

    /// <summary>
    /// 创建 目标和朝向
    /// </summary>
    public static void AddPosition(Vector3 positon, AssemblyEntityBase owner)
    {
        AssemblyPosition data = CreateAssembly<AssemblyPosition>(EnumAssemblyType.Position, owner);
        AssemblyAdd(data, owner);
        data.SetPosition(positon);
    }

    public static void AddViewRole(PrototypeRole role, AssemblyEntityBase owner)
    {
        AddView(role.Name, role.Path, DefineLayer.ENTITY, owner);
    }
    public static AssemblyView AddView(string strName, string path, string layer, AssemblyEntityBase owner)
    {
        AssemblyView data = CreateAssembly<AssemblyView>(EnumAssemblyType.View, owner);
        data.SetPath(strName, path, layer);
        AssemblyAdd(data, owner);
        return data;
    }
    public static AssemblyAnimator AddAnimator(EnumAnimator value, int type, AssemblyEntityBase owner)
    {
        AssemblyAnimator data = CreateAssembly<AssemblyAnimator>(EnumAssemblyType.Animator, owner);
        data.SetType(type);
        data.SetValue(value);
        AssemblyAdd(data, owner);
        return data;
    }

    /// <summary>
    /// 移除组件
    /// </summary>
    public static void AssemblyRemove(EnumAssemblyType type, AssemblyEntityBase owner)
    {
        if (owner.ContainsKey(type))
        {
            IAssembly assembly = owner.GetData(type);
            owner.Remove(type);
            owner.NotifyObserver(EnumAssemblyOperate.Remove, assembly);
            assembly.OnRelease();
            MessageDispatcher.SendMessage(owner, DefineNotification.ASSEMBLY_REM, assembly);
        }
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T AssemblyAdd<T>(T data, AssemblyEntityBase owner) where T : AssemblyBase, new()
    {
        if (!owner.ContainsKey(data.AssemblyType))
        {
            owner.AddData(data);
            //MessageDispatcher.SendMessage(owner, DefineNotification.ASSEMBLY_ADD, rData: data);
            return data;
        }
        return null;
    }
    /// <summary>
    /// 创建组件
    /// </summary>
    public static T CreateAssembly<T>(EnumAssemblyType type, AssemblyEntityBase owner) where T : AssemblyBase, new()
    {
        T data = new T();
        data.OnInit(type, owner);
        return data;
    }


}