using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeAttribute:BasePrototype
{
    public string SimpleName { get;private set;}

    protected override void OnLoadData(XmlNode data)
    {
        SimpleName = Utility.Xml.GetAttribute<string>(data, "SimpleName");

    }
}