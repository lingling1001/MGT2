using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMonoView : MonoBehaviour
{
    public int EntityId;
    public EntityAssembly Entity;
    public void Link(EntityAssembly data)
    {
        if (data == null)
        {
            return;
        }
        Entity = data;
        EntityId = data.EntityId;
    }
   

}
