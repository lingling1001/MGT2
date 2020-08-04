using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEntity
{

    public static AssemblyEntityBase CreateEntity(int entityId)
    {
        AssemblyEntityBase t = new AssemblyEntityBase();
        t.SetEntityId(entityId);
        FactoryAssembly.AddEntityId(entityId, 0, t);
        return t;
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public static void InitialData(int camp, PrototypeRole data, List<int[]> attributes, AssemblyEntityBase entity)
    {
        FactoryAssembly.AddCamp(camp, entity);
        FactoryAssembly.AddAttribute(attributes, entity);
        FactoryAssembly.AddPrototypeRole(data, entity);
        FactoryAssembly.AddRoleMove(entity);
        AssemblyRole assemblyRole = FactoryAssembly.AddRole(entity);

        FactoryAssembly.AddAbilities(entity, data.GetBasicAbility());




    }



    /// <summary>
    /// 初始化显示
    /// </summary>
    public static void InitialView(Vector3 pos, AssemblyRole entity)
    {
        int modelType = entity.AssyPrototypeRole.Value.ModelType;
        FactoryAssembly.AddPosition(pos, entity.Owner);
        FactoryAssembly.AddDirection(Vector3.forward, entity.Owner);
        FactoryAssembly.AddViewRole(entity.AssyPrototypeRole.Value, entity.Owner);
        FactoryAssembly.AddAnimator(EnumAnimator.Idle, modelType, entity.Owner);
        FactoryAssembly.AddEyeSensor(entity.Owner);
        FactoryAssembly.AddTransRoleHead(modelType, entity.Owner);
        FactoryAssembly.AddHeadUIItem(entity.Owner);
    }


    /// <summary>
    /// 初始化AI
    /// </summary>
    public static void InitialGoap(AssemblyRole entity)
    {
        GoapRole role = FactoryEntity.CreateIGoap<GoapRole>();
        FactoryAssembly.AddAgent(GoapAgent.CreateAgent(), role, entity.Owner);
        role.InitialRoleAction(entity);

    }

    public static void ReleaseEnitity(AssemblyEntityBase entity)
    {
        Array arrs = Enum.GetValues(typeof(EnumAssemblyType));
        foreach (var item in arrs)
        {
            FactoryAssembly.AssemblyRemove((EnumAssemblyType)item, entity);
        }

    }

    public static void ReleaseEnitityView(AssemblyRole entity)
    {

        FactoryAssembly.AssemblyRemove(EnumAssemblyType.GoapAgent, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.View, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.Position, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.Direction, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.Animator, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.EyeSensor, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.EntityDead, entity.Owner);
        FactoryAssembly.AssemblyRemove(EnumAssemblyType.HeadUIItem, entity.Owner);

        entity.ClearCache();

        //FactoryAssembly.AssemblyRemove(EnumAssemblyType.EntityDead, entity.Owner);


    }




    public static T CreateIGoap<T>() where T : IGoap, new()
    {

        return new T();
    }


}
