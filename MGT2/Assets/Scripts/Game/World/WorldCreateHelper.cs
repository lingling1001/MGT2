using System.Collections.Generic;
using UnityEngine;

public class WorldCreateHelper
{


    public static void CreateEntities()
    {

        for (int cnt = 0; cnt < 5; cnt++)
        {
            Vector3 pos = GameManager<MapManager>.QGetOrAddMgr().FindPath.CenterPosition;
            Vector3 newPos = pos;
            if (cnt != 0)
            {
                newPos = GameHelper.GetRangePosition(pos, -20, 20);
            }
            EntityAssembly entity = CreateEntity(newPos, AssetsName.TempMod);
            AssemblyCache cache = entity.GetData<AssemblyCache>();
            AssemblyRoleInfo role = EntityFactory.AssemblyCreateAdd<AssemblyRoleInfo>(entity);
            EnumGender gen = CreateRoleHelper.GetRandomGender();
            string strIcon = CreateRoleHelper.GetRandomHeadIcon(gen);
            role.SetData(CreateRoleHelper.GetRandomName(gen), strIcon, gen);

            AssemblyAttribute attribute = EntityFactory.AssemblyCreateAdd<AssemblyAttribute>(entity);
            List<AttributeData> list = CreateAttributeDatas();
            attribute.Initial(list);

            EntityFactory.AssemblyCreateAdd<AssemblyHeadUI>(entity);
            EntityFactory.AssemblyCreateAdd<AssemblyRoleAction>(entity);
            if (cnt == 0)
            {
                EntityFactory.AssemblyCreateAdd<AssemblyCameraFollow>(entity);
                EntityFactory.AssemblyCreateAdd<AssemblyRoleControl>(entity);
                cache.AssyRoleControl.SetRoleType(EnumRoleControl.Self);
                cache.AssyRoleAction.SetActionType(EnumRoleAction.Patrol);
            }

        }

    }

    public static EntityAssembly CreateEntity(Vector3 pos, string strPath)
    {
        EntityAssembly entity = EntityFactory.CreateEntityToMap();
        AssemblyPosition assyPos = EntityFactory.AssemblyCreateAdd<AssemblyPosition>(entity);
        assyPos.SetPosition(pos);
        AssemblyView assyView = EntityFactory.AssemblyCreateAdd<AssemblyView>(entity);
        assyView.SetPath(strPath);

        EntityFactory.AssemblyCreateAdd<AssemblyCache>(entity);

        return entity;
    }

    public static List<AttributeData> CreateAttributeDatas()
    {
        List<AttributeData> list = new List<AttributeData>();

        list.Add(EntityFactory.CreateAttribute(DTAttribute.HP, Random.Range(100, 150)));
        list.Add(EntityFactory.CreateAttribute(DTAttribute.Attack, Random.Range(7, 15)));
        list.Add(EntityFactory.CreateAttribute(DTAttribute.SpeedMove, Random.Range(2, 3)));
        list.Add(EntityFactory.CreateAttribute(DTAttribute.RangeGuard, Random.Range(2, 5)));
        list.Add(EntityFactory.CreateAttribute(DTAttribute.RangeAttack, 1));

        return list;
    }
}