using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIWorldOperateRole : MonoPoolItem, IWorldNodeable
{
    public List<UIWorldOperateItem> _listMenuItems = new List<UIWorldOperateItem>();
    public EnumWorldResNode ResNodeType { get; private set; }

    private void Awake()
    {
        GetBindComponents(gameObject);
    }

    public void OnInit(EnumWorldResNode type, AssemblyCache resInfo)
    {

        RefreshItemRole.Refresh(m_Scr_RoleItem, resInfo.AssyRoleInfo);

    }
    public void OnClickMenu(EnumWorldResTP type)
    {

    }

    public void OnRelease()
    {
        for (int cnt = 0; cnt < _listMenuItems.Count; cnt++)
        {
            NGUITools.SetActive(_listMenuItems[cnt], false);
        }
    }
}
