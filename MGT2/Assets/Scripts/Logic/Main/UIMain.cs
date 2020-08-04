using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;

public partial class UIMain : BaseUI
{

    private List<UIMainButtonItem> _listItems = new List<UIMainButtonItem>();
    private GameObject _prefabItem;
    public override void OnInit()
    {
        base.OnInit();
        GetBindComponents(ObjUI);
        //InitialButtons();

        m_Btn_Close.RegistEvent(EventClickBtnClose);
    }

    private void EventClickBtnClose()
    {
            GameStateManager.Instance.FsmGameState.ChangeState(FsmManagerGame.GAME_STATE_MAIN);

    }

    private void InitialButtons()
    {
        List<PrototypeMainUI> list = PrototypeManager<PrototypeMainUI>.Instance.GetTableList();
        UIHelper.SetListData(_listItems, list, EventGetItem, EventSetItem);

        m_GGroup_TopRight.CalculateLayoutInputVertical();


    }

    private void EventSetItem(UIMainButtonItem arg1, PrototypeMainUI arg2)
    {
        arg1.SetData(arg2);
    }



    private UIMainButtonItem EventGetItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>("UI/Main/UIMainButtonItem.prefab");
        }
        if (_prefabItem != null)
        {
            GameObject obj = NGUITools.AddChild(m_GGroup_TopRight.gameObject, _prefabItem);
            UIMainButtonItem item = obj.GetOrAddComponent<UIMainButtonItem>();
            item.SetClickCallback(EventClickButton);
            return item;
        }
        return null;
    }



    private void EventClickButton(UIMainButtonItem obj)
    {
        if (obj.Data.EventType == DefineMainUIType.EXIT)
        {
            GameStateManager.Instance.FsmGameState.ChangeState(FsmManagerGame.GAME_STATE_MAIN);
            return;
        }
        if (obj.Data.EventType == DefineMainUIType.SETTING)
        {
            //   Application.Quit();
            return;
        }

    }



    [ContextMenu("Execute")]
    public void SetTopThis()
    {
        UIDepthHelper.Set2Top(this);

    }
}
