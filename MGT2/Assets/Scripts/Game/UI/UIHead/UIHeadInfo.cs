using com.ootii.Messages;
using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIHeadInfo : BaseUI, IUpdate
{
    private GameObject _prefabItem;
    private Dictionary<int, UIHeadInfoItem> _mapItems = new Dictionary<int, UIHeadInfoItem>();

    public int Priority => throw new NotImplementedException();

    public override void OnInit()
    {
        GetBindComponents(ObjUI);
        MessageDispatcher.AddListener(NotificationName.EventEntityHeadAdd, EventEntityHeadAdd);
        MessageDispatcher.AddListener(NotificationName.EventEntityHeadRem, EventEntityHeadRem);

        RegisterInterfaceManager.RegisteUpdate(this);

        RefreshAllEntity();
    }
    private void RefreshAllEntity()
    {
        Dictionary<int, EntityAssembly> map = GameManager.QGetOrAddMgr<EntityManager>().GetAllDatas();

        Dictionary<int, EntityAssembly> targets = new Dictionary<int, EntityAssembly>();
        foreach (var item in map)
        {
            if (item.Value.ContainsKey<AssemblyHeadUI>())
            {
                targets.Add(item.Key, item.Value);
            }
        }

        UIHelper.SetItemsDictionary(_mapItems, targets, EventGetItem, EventSetData, EventGetKey);


    }

    private void EventEntityHeadRem(IMessage rMessage)
    {
        EntityAssembly entity = rMessage.Data as EntityAssembly;
        if (entity == null)
        {
            return;
        }
        if (!_mapItems.ContainsKey(entity.EntityId))
        {
            return;
        }
        NGUITools.SetActive(_mapItems[entity.EntityId], false);
    }


    private void EventEntityHeadAdd(IMessage rMessage)
    {
        EntityAssembly entity = rMessage.Data as EntityAssembly;
        if (entity == null)
        {
            return;
        }
        if (_mapItems.ContainsKey(entity.EntityId))
        {
            return;
        }
        UIHeadInfoItem item = EventGetItem();
        EventSetData(entity.EntityId, item, entity);
        _mapItems.Add(entity.EntityId, item);
    }

    private void EventClickHeadInfo(UIHeadInfoItem obj)
    {
        if (obj.AssemblyCache == null)
        {
            return;
        }
        if (UIManager.Instance.UIIsOpen<UIWorldOperate>())
        {
            UIManager.Instance.CloseUI<UIWorldOperate>();
            return;
        }
        UIManager.Instance.OpenUI<UIWorldOperate>(obj.AssemblyCache);
    }

    private UIHeadInfoItem EventGetItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>(AssetsName.UIHeadInfoItem);
        }
        GameObject obj = NGUITools.AddChild(ObjUI, _prefabItem);
        UIHeadInfoItem item = obj.AddMissingComponent<UIHeadInfoItem>();
        item.SetClickEvent(EventClickHeadInfo);
        return item;
    }

    private void EventSetData(int arg1, UIHeadInfoItem arg2, EntityAssembly arg3)
    {
        arg2.SetData(arg3);
    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        foreach (var item in _mapItems)
        {
            if (NGUITools.GetActive(item.Value))
            {
                item.Value.RefreshPosition();
            }
        }
    }

    private int EventGetKey()
    {
        int key = -_mapItems.Count;
        while (_mapItems.ContainsKey(key))
        {
            key--;
        }
        return key;

    }

    public override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        MessageDispatcher.RemoveListener(NotificationName.EventEntityHeadAdd, EventEntityHeadAdd);
        MessageDispatcher.RemoveListener(NotificationName.EventEntityHeadRem, EventEntityHeadRem);
        base.OnRelease();
    }


}
