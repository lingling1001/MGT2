using com.ootii.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIRoleControl : BaseUI
{
    private EntityManager _entityMgr;
    private List<UIRoleControlItem> _listItems = new List<UIRoleControlItem>();
    public override void OnInit()
    {
        MessageDispatcher.AddListener(NotificationName.EventEntityInitial, EventEntityInitial);
        _entityMgr = GameManager.QGetOrAddMgr<EntityManager>();
        GetBindComponents(ObjUI);
        RefreshContent();
    }

    private void EventEntityInitial(IMessage rMessage)
    {
        //RefreshContent();
    }

    private void RefreshContent()
    {
        List<AssemblyRoleControl> list = _entityMgr.GetListDatas<AssemblyRoleControl>();

        UIHelper.SetItemsList(_listItems, list, EventGetItem, EventSetData);

    }


    private GameObject _prefabItem;

    private UIRoleControlItem EventGetItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>(AssetsName.UIRoleControlItem);
        }
        GameObject obj = NGUITools.AddChild(m_HGroup_Role.gameObject, _prefabItem);
        return obj.AddMissingComponent<UIRoleControlItem>();
    }
    private void EventSetData(UIRoleControlItem arg2, AssemblyRoleControl arg3)
    {
        arg2.SetData(arg3);
    }

    public override void OnRelease()
    {
        MessageDispatcher.RemoveListener(NotificationName.EventEntityInitial, EventEntityInitial);
    }
}
