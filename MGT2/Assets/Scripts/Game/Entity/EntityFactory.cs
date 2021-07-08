using System.Collections.Generic;
public class EntityFactory
{

    /// <summary>
    /// 创建实体并且添加到管理器中
    /// </summary>
    /// <returns></returns>
    public static EntityAssembly CreateEntityToMap(int type = 0)
    {
        EntityManager entityMgr = GameManager<EntityManager>.QGetOrAddMgr();
        int key = entityMgr.GetFreeEntityKey();
        EntityAssembly entity = CreateEntity(key, type);
        entityMgr.Addition(entity);
        return entity;
    }



    /// <summary>
    /// 创建组件并添加到entity
    /// </summary>
    public static T AssemblyCreateAdd<T>(EntityAssembly owner) where T : AssemblyBase, new()
    {
        T data = AssemblyCreate<T>();
        AssemblyAddBase(owner, data);
        return data;
    }
    public static void AssemblyAddBase(EntityAssembly owner, AssemblyBase assy)
    {
        owner.AddData(assy);
        assy.Init(owner);
    }

    /// <summary>
    /// 创建实体
    /// </summary>
    public static EntityAssembly CreateEntity(int id, int type)
    {
        EntityAssembly entity = new EntityAssembly();
        entity.SetEntityId(id, type);
        return entity;
    }

    /// <summary>
    /// 创建组件
    /// </summary>
    public static T AssemblyCreate<T>() where T : IAssembly, new()
    {
        return new T();
    }
    /// <summary>
    /// 创建属性数据
    /// </summary>
    public static AttributeData CreateAttribute(int key, int value)
    {
        return new AttributeData(key, value);
    }
}