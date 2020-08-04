using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UISelectRole : BaseUI
{
    private List<UISelectRoleItem> _listItems = new List<UISelectRoleItem>();
    public override void OnInit()
    {
        base.OnInit();
        GetBindComponents(ObjUI);
        UIDepthHelper.Set2Top(this);
        m_Btn_LogIn.RegistEvent(EventClickBtnLogIn);
        m_Btn_Close.RegistEvent(EventClickBtnClose);

        InitalDefaultRole();

        SelectRole(_listItems[1].Data.PrototypeId);

    }

    private void InitalDefaultRole()
    {
        int[] arrs = PrototypeGameConfig.GetConfigList<int>(EnumGameConfig.DefaultRole);

        UIHelper.SetListData(_listItems, arrs, EventGetItem, EventSetData);

        m_GGroup_RoleParent.CalculateLayoutInputHorizontal();


    }

    private void SelectRole(int id)
    {
        for (int cnt = 0; cnt < _listItems.Count; cnt++)
        {
            _listItems[cnt].SetValue(_listItems[cnt].Data.PrototypeId == id);
        }

    }

    private void EventClickItemButton(UISelectRoleItem obj)
    {

        SelectRole(obj.Data.PrototypeId);

    }


    private void EventSetData(UISelectRoleItem arg1, int arg2)
    {
        PrototypeRole data = PrototypeManager<PrototypeRole>.Instance.GetPrototype(arg2);
        arg1.SetData(data);
    }


    private GameObject _prefabItem;
    private UISelectRoleItem EventGetItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>(AssetsName.UI_SELECT_ROLE_ITEM);
        }
        if (_prefabItem != null)
        {
            GameObject obj = NGUITools.AddChild(m_GGroup_RoleParent.gameObject, _prefabItem);
            UISelectRoleItem item = obj.GetOrAddComponent<UISelectRoleItem>();
            item.SetClickCallback(EventClickItemButton);
            return item;
        }
        return null;
    }


    private void EventClickBtnClose()
    {
        Application.Quit();
    }

    private void EventClickBtnLogIn()
    {
        PrototypeRole data = _listItems.Find(item => item.IsSelect).Data;
        AssemblyRole assemblyRole = RoleManager.Instance.CreateAssemblyRole(data);

        RoleManager.Instance.SelectRole(assemblyRole);

        GameStateManager.Instance.FsmGameState.ChangeState(FsmManagerGame.GAME_STATE_START);
    }
}
