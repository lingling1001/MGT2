using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDataEntity : AgentDataBase
{
    public EntityAssembly Entity { get; private set; }
    public void SetEntity(EntityAssembly entity)
    {
        Entity = entity;
    }
    public AssemblyCache GetEntityCache()
    {
        if (Entity != null)
        {
            return Entity.GetData<AssemblyCache>();
        }
        return null;
    }
}
