using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMonoView : MonoBehaviour
{
    public int EntityId;
    public AssemblyEntityBase Entity;
    public void Link(AssemblyEntityBase data)
    {
        if (data == null)
        {
            return;
        }
        Entity = data;
        EntityId = data.GetData<AssemblyRole>(EnumAssemblyType.Role).EntityId;
    }
   
}
