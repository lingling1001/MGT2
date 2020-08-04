using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;

public class UIItemNormalScr : MonoBehaviour
{
    [System.Serializable]
    public class UIItemMemberInfo
    {
        public EItemInfoType m_itemInfoType; //唯一
        public EItemMemberType m_memberType;
        public MonoBehaviour m_mono;
    }
    public enum EItemMemberType : byte
    {
        UILabel,
        UITexture,
        Max,
    }
    public enum EItemInfoType : byte
    {
        Quality = 0,
        Name,
        Icon,
        Desc,
        Lv,
        Num,
        Flag,
        Frame,
        Event,
        Max,
    }

    [HideInInspector]
    [SerializeField]
    public List<UIItemMemberInfo> m_itemMembers = new List<UIItemMemberInfo>();
    [HideInInspector]
    [SerializeField]
    public Button m_button;
    [HideInInspector]
    [SerializeField]
    public bool m_needShowTips = false;

    private int m_itemType = 0;
    public int ItemType { get { return m_itemType; } }

    private int m_itemId;
    public int ItemId { get { return m_itemId; } }
    private object m_param;
    public object Param { get { return m_param; } }
    void Awake()
    {
        //Reset();
        if (m_needShowTips && m_button != null)
        {
            m_button.onClick.AddListener(OnPressShowTipsPanel);//RegistEvent(OnPressShowTipsPanel);
        }
    }

    

    public void Reset()
    {
        ResetList(m_itemMembers);
    }
    private void ResetList(List<UIItemMemberInfo> list)
    {
        if (null == list || list.Count == 0) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            UIItemMemberInfo val = list[i];
            if (null == val || null == val.m_mono) { continue; }
            val.m_mono.gameObject.SetActive(false);
        }
    }
    public UIItemMemberInfo GetMemberInfo(EItemInfoType key)
    {
        for (int i = 0; i < m_itemMembers.Count; i++)
        {
            UIItemMemberInfo val = m_itemMembers[i];
            if (null != val && val.m_itemInfoType == key)
            {
                return val;
            }
        }
        return null;
    }
    public void SetScrMemberInfo(EItemInfoType key, bool active, string info = "")
    {
        UIItemMemberInfo memberInfo = GetMemberInfo(key);
        if (memberInfo == null)
        {
            return;
        }
        SetScrMemberInfo(memberInfo, active, info);
    }
    public bool SetScrMemberInfoActive(EItemInfoType key, bool active)
    {
        UIItemMemberInfo memberInfo = GetMemberInfo(key);
        if (memberInfo == null) { return false; }
        return SetScrMemberInfoActive(memberInfo.m_mono, active);
    }

    public bool SetScrMemberInfoActive(MonoBehaviour mono, bool active)
    {
        if (mono == null)
        {
            return false;
        }
        if (mono.gameObject.activeSelf != active)
        {
            mono.gameObject.SetActive(active);
        }
        return active;
    }

    public void SetScrMemberInfo(UIItemMemberInfo memberInfo, bool active, string info = "")
    {
        MonoBehaviour mono = memberInfo.m_mono;
        if (!SetScrMemberInfoActive(mono, active))
        {
            return;//隐藏不显示 直接返回
        }
        switch (memberInfo.m_memberType)
        {
            case EItemMemberType.UILabel:
                TMP_Text label = mono as TMP_Text;
                if (null != label) { label.text = info; }
                break;
            case EItemMemberType.UITexture:
                Image uiTexture = mono as Image;
                //AllianceBossHelper.SetTextureIcon(uiTexture, info);
                break;
            default:
                break;
        }

    }
    public bool SetScrMemberColor(EItemInfoType key, Color color)
    {
        UIItemMemberInfo memberInfo = GetMemberInfo(key);
        if (memberInfo == null) { return false; }
        SetScrMemberColor(memberInfo, color);
        return true;
    }
    public void SetScrMemberColor(UIItemMemberInfo memberInfo, Color color)
    {
        if (memberInfo == null)
        {
            return;
        }
        Image widget = memberInfo.m_mono as Image;
        if (widget == null)
        {
            return;
        }
        widget.color = color;
    }

    public void SetItemId(int itemId, int itemType = 1, object param = null)
    {
        m_itemId = itemId;
        m_itemType = itemType;
        m_param = param;
    }

    private void OnPressShowTipsPanel()
    {
        UIItemNormalScrTips.ShowTips(ItemType, ItemId, Param, gameObject);
    }
}