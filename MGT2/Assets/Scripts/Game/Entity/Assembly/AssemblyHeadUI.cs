using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyHeadUI : AssemblyBase
{
    protected override void OnInit(EntityAssembly owner)
    {
        base.OnInit(owner);
        MessageDispatcher.SendMessage(NotificationName.EventEntityHeadAdd, Owner);
    }
    protected override void OnRelease()
    {
        MessageDispatcher.SendMessage(NotificationName.EventEntityHeadRem, Owner);
        base.OnRelease();
    }
}
