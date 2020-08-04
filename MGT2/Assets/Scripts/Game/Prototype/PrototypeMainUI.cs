using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeMainUI:BasePrototype
{
    public int EventType { get;private set;}
    public int ParentType { get;private set;}
    public int IsShow { get;private set;}

    protected override void OnLoadData(XmlNode data)
    {
        EventType = Utility.Xml.GetAttribute<int>(data, "EventType");
        ParentType = Utility.Xml.GetAttribute<int>(data, "ParentType");
        IsShow = Utility.Xml.GetAttribute<int>(data, "IsShow");

    }
}