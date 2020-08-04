using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeWeapon:BasePrototype
{
    public int Type { get;private set;}
    public string ModelPath { get;private set;}

    protected override void OnLoadData(XmlNode data)
    {
        Type = Utility.Xml.GetAttribute<int>(data, "Type");
        ModelPath = Utility.Xml.GetAttribute<string>(data, "ModelPath");

    }
}