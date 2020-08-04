using com.ootii.Messages;
using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager>, IInit, IUpdate
{
    public int Priority { get { return DefinePriority.NORMAL; } }

    public void OnInit()
    {
        RegisterInterfaceManager.RegisteUpdate(this);
    }

    public void OnRelease()
    {
        MessageDispatcher.ClearMessages();
        MessageDispatcher.ClearListeners();
        RegisterInterfaceManager.UnRegisteUpdate(this);
    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        MessageDispatcher.Update();
    }
}
