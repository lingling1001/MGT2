using UnityEngine;

public class FactoryGoapActionData
{
    public static T Create<T>(EnumGADType type, bool value = false) where T : GADataBase, new()
    {
        T t = new T();
        t.SetType(type);
        t.SetValue(value);
        return t;
    }


    public static GADEntity CreateSelfEntity(AssemblyRole entity)
    {
        GADEntity data = Create<GADEntity>(EnumGADType.SelfEntity);
        data.SetEntity(entity);
        return data;
    }

    public static GADRolePatrol CreateGADRolePatrol(bool value = false)
    {
        GADRolePatrol data = Create<GADRolePatrol>(EnumGADType.Patrol, value);
        return data;
    }

    //public static GADataBase CreateIdle(bool value = false)
    //{
    //    GADataBase data = Create<GADataBase>(EnumGADType.Patrol, value);
    //    return data;
    //}

    //public static ActionDataDestroyEnemy CreateDestroyEnemy()
    //{
    //    ActionDataDestroyEnemy data = Create<ActionDataDestroyEnemy>(EnumGAName.DestroyEnemy);
    //    return data;
    //}
}
