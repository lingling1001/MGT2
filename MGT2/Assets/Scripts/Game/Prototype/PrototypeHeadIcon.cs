using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeHeadIcon:BasePrototype
{
    public int Type { get;private set;}

    protected override void OnLoadData(XmlNode data)
    {
        Type = Utility.Xml.GetAttribute<int>(data, "Type");
    }
}