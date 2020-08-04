using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 地图中的角色管理类
/// </summary>
public class UIPlaceRole : BaseUI
{
    private GridLayoutGroup _gridParent;
    private GameObject _prefabItem;
    private List<UIPlaceRoleItem> _listItems = new List<UIPlaceRoleItem>();
    public override void OnInit()
    {
        base.OnInit();

        return;
        UIDepthHelper.Set2Top(this);
        _gridParent = this.Find<GridLayoutGroup>("gridParent");

        List<AssemblyRole> list = RoleManager.Instance.ListPlayers ;

        UIHelper.SetListData(_listItems, list, EventGetItem, EventSetItem);

        _gridParent.CalculateLayoutInputVertical();


    }
    private void EventSetItem(UIPlaceRoleItem arg1, AssemblyRole arg2)
    {
        arg1.SetData(arg2);
        arg1.SetClickCallback(EventClickItem);
    }
    /// <summary>
    /// 点击Item事件，生成角色
    /// </summary>
    private void EventClickItem(UIPlaceRoleItem obj)
    {

        PlaceRoleManager.Instance.PutRoleToMap(obj.EntityData);

    }

    private UIPlaceRoleItem EventGetItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>(UIHelper.GetUIPath(EnumUIType.UIPlaceRoleItem));
        }
        if (_prefabItem != null)
        {
            GameObject obj = NGUITools.AddChild(_gridParent.gameObject, _prefabItem);
            return obj.GetOrAddComponent<UIPlaceRoleItem>();
        }
        return null;
    }


    //[ContextMenu("Execute")]
    //public void SetTopThis()
    //{
    //    UIDepthHelper.Set2Top(this);

    //}
}
