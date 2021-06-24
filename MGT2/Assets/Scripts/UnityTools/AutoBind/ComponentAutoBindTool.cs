using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 组件自动绑定工具
/// </summary>
public class ComponentAutoBindTool : MonoBehaviour
{

#if UNITY_EDITOR
    [Serializable]
    public class BindData
    {
        public BindData()
        {
        }

        public BindData(string name, Component bindCom)
        {
            Name = name;
            BindCom = bindCom;
        }

        public string Name;
        public Component BindCom;
    }

    public List<BindData> BindDatas = new List<BindData>();

    [SerializeField]
    private string m_ClassName;

    [SerializeField]
    private string m_Namespace;

    [SerializeField]
    private string m_CodePath;

    public string ClassName
    {
        get
        {
            return m_ClassName;
        }
    }

    public string Namespace
    {
        get
        {
            return m_Namespace;
        }
    }

    public string CodePath
    {
        get
        {
            return m_CodePath;
        }
    }

    public IAutoBindRuleHelper RuleHelper
    {
        get;
        set;
    }
#endif

    [SerializeField]
    public List<Component> m_BindComs = new List<Component>();


    public T GetBindComponent<T>(int index) where T : Component
    {
        if (index >= m_BindComs.Count)
        {
            Log.Error("索引无效 Index {0} Count {1} {2}", index, m_BindComs.Count, gameObject.name);
            return null;
        }
        T bindCom = m_BindComs[index] as T;
        if (bindCom == null)
        {
            Log.Error("类型无效 Index {0} Type {1} {2}", index, typeof(T),gameObject.name);
            return null;
        }
        return bindCom;
    }
}
