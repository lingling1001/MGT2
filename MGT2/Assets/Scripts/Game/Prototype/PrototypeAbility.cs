using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeAbility : BasePrototype
{
    public string Icon { get; private set; }
    public string Describe { get; private set; }
    public int Type { get; private set; }
    public string Limit { get; private set; }
    public string EffectGroup { get; private set; }

    protected override void OnLoadData(XmlNode data)
    {
        Type = Utility.Xml.GetAttribute<int>(data, "Type");
        Icon = Utility.Xml.GetAttribute<string>(data, "Icon");
        Describe = Utility.Xml.GetAttribute<string>(data, "Describe");
        Limit = Utility.Xml.GetAttribute<string>(data, "Limit");
        EffectGroup = Utility.Xml.GetAttribute<string>(data, "EffectGroup");
    }

    private string[] _effectGroup;
    /// <summary>
    /// 攻击效果
    /// </summary>
    public string[] GetEffect()
    {
        if (_effectGroup == null)
        {
            _effectGroup = Utility.Xml.ParseString<string>(EffectGroup, Utility.Xml.SplitVerticalBar);
        }
        return _effectGroup;
    }

    private string[] _limit;
    /// <summary>
    /// 释放 限制
    /// </summary>
    public string[] GetLimit()
    {
        if (_limit == null)
        {
            _limit = Utility.Xml.ParseString<string>(Limit, Utility.Xml.SplitVerticalBar);
        }
        return _limit;
    }
}