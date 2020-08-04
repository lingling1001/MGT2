using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;
using UnityEngine;

public class PrototypeRole : BasePrototype
{
    public int Type { get; private set; }
    public int ModelType { get; private set; }
    public string Path { get; private set; }
    public int Quality { get; private set; }
    public string BasicAbility { get; private set; }
    public Vector3 UIRotate { get; private set; }
    public Vector3 UIPosition { get; private set; }
    public string BasicAttribute { get; private set; }
    public string LoadWeaponIds { get; private set; }


    protected override void OnLoadData(XmlNode data)
    {
        Type = Utility.Xml.GetAttribute<int>(data, "Type");
        ModelType = Utility.Xml.GetAttribute<int>(data, "ModelType");
        Path = Utility.Xml.GetAttribute<string>(data, "Path");
        Quality = Utility.Xml.GetAttribute<int>(data, "Quality");
        BasicAbility = Utility.Xml.GetAttribute<string>(data, "BasicAbility");
        UIRotate = GetAttribut_vector3(data, "UIRotate");
        UIPosition = GetAttribut_vector3(data, "UIPosition");
        BasicAttribute = Utility.Xml.GetAttribute<string>(data, "BasicAttribute");
        LoadWeaponIds = Utility.Xml.GetAttribute<string>(data, "LoadWeaponIds");
    }
    private List<int[]> _listAttribute = null;

    /// <summary>
    /// 获取属性集合
    /// </summary>
    public List<int[]> GetListAttribute()
    {
        if (_listAttribute == null)
        {
            _listAttribute = new List<int[]>();
            string[] strs = Utility.Xml.ParseString<string>(BasicAttribute, Utility.Xml.SplitVerticalBar);
            if (strs == null || strs.Length == 0)
            {
                return _listAttribute;
            }
            for (int cnt = 0; cnt < strs.Length; cnt++)
            {
                int[] arrs = Utility.Xml.ParseString<int>(strs[cnt], Utility.Xml.SplitComma);
                if (arrs == null || arrs.Length != 2)
                {
                    continue;
                }
                _listAttribute.Add(arrs);
            }
        }
        return _listAttribute;
    }

    private List<int> _listLoadWeaponIds;
    /// <summary>
    /// 挂载武器列表
    /// </summary>
    private List<int> GetLoadWeaponIds()
    {
        if (_listLoadWeaponIds == null)
        {
            int[] arrs = Utility.Xml.ParseString<int>(LoadWeaponIds, Utility.Xml.SplitComma);
            _listLoadWeaponIds = new List<int>();
            if (arrs != null && arrs.Length > 0)
            {
                _listLoadWeaponIds.AddRange(arrs);
            }
        }
        return _listLoadWeaponIds;
    }

    private int[] _basicAbility;
    public int[] GetBasicAbility()
    {
        if (_basicAbility == null)
        {
            _basicAbility = Utility.Xml.ParseString<int>(BasicAbility, Utility.Xml.SplitComma);
        }
        return _basicAbility;
    }
}