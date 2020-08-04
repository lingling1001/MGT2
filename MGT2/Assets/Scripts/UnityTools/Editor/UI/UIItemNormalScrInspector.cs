using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(UIItemNormalScr))]
public class UIItemNormalScrInspector : Editor
{
    private UIItemNormalScr m_normalScr;
    private List<int> m_itemTypeInfoList = new List<int>(); 
    private List<string> m_itemTypeInfoStrList = new List<string>(); 
    private List<int> m_itemMemberTypeList = new List<int>();
    private List<string> m_itemMemberTypeStrList = new List<string>();
    private List<int> m_list = new List<int>();
    private List<string> m_strList = new List<string>();

    void Awake()
    {
        m_normalScr = target as UIItemNormalScr;
    }

    void OnEnable()
    {
        m_normalScr = target as UIItemNormalScr;
        m_itemMemberTypeList.Clear();
        m_itemMemberTypeStrList.Clear();
        for (int i = 0; i < (int)UIItemNormalScr.EItemMemberType.Max; i++)
        {
            m_itemMemberTypeList.Add(i);
            m_itemMemberTypeStrList.Add(((UIItemNormalScr.EItemMemberType)i).ToString());
        }
        FreshItemInfoTypeList();
    }

    public override void OnInspectorGUI()
    {
        if (null == m_normalScr) { return; }
        EditorGUILayout.BeginVertical();
        EditorGUI.BeginChangeCheck();


        if (NGUIEditorTools.DrawHeader("Element", "Element", false, true))
        {
            NGUIEditorTools.BeginContents(true);
            for (int i = 0; i < m_normalScr.m_itemMembers.Count; i++)
            {
                SetMonoItemInfo(m_normalScr.m_itemMembers[i]);
            }
            NGUIEditorTools.EndContents();
        }
        //m_normalScr.m_needButton = EditorGUILayout.Toggle("是否需要按钮", m_normalScr.m_needButton);
        //if (m_normalScr.m_needButton)
        //{
        //    m_normalScr.m_button = EditorGUILayout.ObjectField("Button", m_normalScr.m_button, typeof(UIButton), true) as UIButton;
        //}
        m_normalScr.m_needShowTips = EditorGUILayout.Toggle("是否需要弹出tips", m_normalScr.m_needShowTips);
        if (m_itemTypeInfoList.Count > 0)
        {
            if (GUILayout.Button("添加"))
            {
                UIItemNormalScr.UIItemMemberInfo info = new UIItemNormalScr.UIItemMemberInfo();
                info.m_itemInfoType = (UIItemNormalScr.EItemInfoType)m_itemTypeInfoList[0];
                info.m_memberType = UIItemNormalScr.EItemMemberType.UILabel;
                info.m_mono = null;
                m_normalScr.m_itemMembers.Add(info);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            FreshItemInfoTypeList();
        }
        EditorGUILayout.EndVertical();
    } 

    private void SetMonoItemInfo(UIItemNormalScr.UIItemMemberInfo memberInfo)
    {
        EditorGUILayout.BeginHorizontal();
        m_list.Clear();
        m_strList.Clear();
        m_list.Add((int)memberInfo.m_itemInfoType);
        m_strList.Add(memberInfo.m_itemInfoType.ToString());
        for (int i = 0; i < m_itemTypeInfoStrList.Count; i++)
        {
            m_list.Add(m_itemTypeInfoList[i]);
            m_strList.Add(m_itemTypeInfoStrList[i]);
        }
        memberInfo.m_itemInfoType =
            (UIItemNormalScr.EItemInfoType)EditorGUILayout.IntPopup((int) memberInfo.m_itemInfoType, m_strList.ToArray(),
                    m_list.ToArray());
        memberInfo.m_memberType =
            (UIItemNormalScr.EItemMemberType)
                EditorGUILayout.IntPopup((int) memberInfo.m_memberType, m_itemMemberTypeStrList.ToArray(),
                    m_itemMemberTypeList.ToArray());
        switch (memberInfo.m_memberType)
        {
            case UIItemNormalScr.EItemMemberType.UILabel:
                memberInfo.m_mono = EditorGUILayout.ObjectField(memberInfo.m_mono, typeof (TMP_Text), true) as TMP_Text;
                break;
            //case UIItemNormalScr.EItemMemberType.UIRenderTextureAtlasIcon:
            //    memberInfo.m_mono = EditorGUILayout.ObjectField(memberInfo.m_mono, typeof(UIRenderTextureAtlasIcon), true) as UIRenderTextureAtlasIcon;
                //break;
            //case UIItemNormalScr.EItemMemberType.UISprite:
            //    memberInfo.m_mono = EditorGUILayout.ObjectField(memberInfo.m_mono, typeof(UISprite), true) as UISprite;
            //    break;
            case UIItemNormalScr.EItemMemberType.UITexture:
                memberInfo.m_mono = EditorGUILayout.ObjectField(memberInfo.m_mono, typeof(Image), true) as Image;
                break;
            default:
                break;
        }

        if (GUILayout.Button("×"))
        {
            m_normalScr.m_itemMembers.Remove(memberInfo);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void FreshItemInfoTypeList()
    {
        m_itemTypeInfoList.Clear();
        m_itemTypeInfoStrList.Clear();
        List<int> list = GetCurSetItemInfoType();
        for (int i = 0; i < (int)UIItemNormalScr.EItemInfoType.Max; i++)
        {
            if (list.Contains(i)) { continue;}
            m_itemTypeInfoList.Add(i);
            m_itemTypeInfoStrList.Add(((UIItemNormalScr.EItemInfoType)i).ToString());
        }
    }

    private List<int> GetCurSetItemInfoType()
    {
        List<int> list = new List<int>();
        if (m_normalScr != null && m_normalScr.m_itemMembers != null)
        {
            for (int i = 0; i < m_normalScr.m_itemMembers.Count; i++)
            {
                list.Add((int)m_normalScr.m_itemMembers[i].m_itemInfoType);
            }
        }
        return list;
    } 
}
