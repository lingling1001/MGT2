using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeText:BasePrototype
{
    public string Chinese { get;private set;}

    protected override void OnLoadData(XmlNode data)
    {
        Chinese = Utility.Xml.GetAttribute<string>(data, "Chinese");

    }
}