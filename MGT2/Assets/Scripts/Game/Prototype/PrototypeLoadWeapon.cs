using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeLoadWeapon : BasePrototype
{
    public int Type { get; private set; }
    public string AniController { get; private set; }
    public string LoadParent { get; private set; }
    public string Rotation { get; private set; }


    protected override void OnLoadData(XmlNode data)
    {
        Type = Utility.Xml.GetAttribute<int>(data, "Type");
        AniController = Utility.Xml.GetAttribute<string>(data, "AniController");
        LoadParent = Utility.Xml.GetAttribute<string>(data, "LoadParent");
        Rotation = Utility.Xml.GetAttribute<string>(data, "Rotation");

    }


}