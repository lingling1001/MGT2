using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIWorldOperateRole : MonoPoolItem, IWorldNodeable
{
    public List<UIWorldOperateItem> _listMenuItems = new List<UIWorldOperateItem>();
    public EnumWorldResNode ResNodeType { get; private set; }
    private AssemblyCache _assemblyCache;
    private void Awake()
    {
        GetBindComponents(gameObject);
    }

    public void OnInit(EnumWorldResNode type, AssemblyCache resInfo)
    {
        RefreshItemRole.Refresh(m_Scr_RoleItem, resInfo.AssyRoleInfo);

        if (resInfo.AssyRoleControl != null)
        {
            AddMenus(EnumWorldResTP.Attack);
            AddMenus(EnumWorldResTP.Guard);
            AddMenus(EnumWorldResTP.Infomation);
        }
        else
        {
            AddMenus(EnumWorldResTP.Infomation);
        }

    }

    private void AddMenus(EnumWorldResTP type)
    {
        string strPath = AssetsName.UIWorldOperateItem;
        Transform parent = m_GGroup_Node.transform;
        UIWorldOperateItem item = ItemPoolMgr.CreateOrGetItem<UIWorldOperateItem>(strPath, parent);
        item.SetClickEvent(EventClickMenu);
        item.SetData(type, _assemblyCache);
        _listMenuItems.Add(item);
    }

    private void EventClickMenu(UIWorldOperateItem obj)
    {

       

    }

    public void OnRelease()
    {
        for (int cnt = 0; cnt < _listMenuItems.Count; cnt++)
        {
            ItemPoolMgr.AddPool(_listMenuItems[cnt]);
        }
        _listMenuItems.Clear();
    }

}
