using com.ootii.Messages;
using MFrameWork;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIHeadManager : MonoSingleton<UIHeadManager>
{
    public Dictionary<int, UIHeadItem> _mapItems = new Dictionary<int, UIHeadItem>();
    private List<UIHeadItem> _listTemp = new List<UIHeadItem>();
    private GameObject _prefabItem;
    public UIHeadItem CreateHeadItem(int id)
    {
        if (_mapItems.ContainsKey(id))
        {
            return null;
        }
        UIHeadItem item;
        if (_listTemp.Count > 0)
        {
            item = _listTemp[0];
            _listTemp.RemoveAt(0);
            UnityObjectExtension.SetActive(item, true);
        }
        else
        {
            item = CreateHeadItem();
        }
        return item;
    }
    private UIHeadItem CreateHeadItem()
    {
        if (_prefabItem == null)
        {
            _prefabItem = ResLoadHelper.LoadAsset<GameObject>(UIHelper.GetUIPath(EnumUIType.UIHeadItem));
        }
        if (_prefabItem != null)
        {
            GameObject obj = NGUITools.AddChild(gameObject, _prefabItem);
            UIHeadItem item = obj.GetOrAddComponent<UIHeadItem>();
            return item;
        }
        return null;
    }
    public void AssemblyRemove(AssemblyHeadUIItem assemblyHeadUIItem)
    {
        AddItemToTemp(assemblyHeadUIItem.Value);
    }
    private void AddItemToTemp(UIHeadItem item)
    {
        if (item == null)
        {
            return;
        }
        _mapItems.Remove(item.EntityId);
        _listTemp.Add(item);
        UnityObjectExtension.SetActive(item, false);
    }
}