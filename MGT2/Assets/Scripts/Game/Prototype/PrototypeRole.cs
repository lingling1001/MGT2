using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;
using UnityEngine;

public class PrototypeRole : BasePrototype
{
    public int ModelType { get; private set; }
    public string Path { get; private set; }
    public string Scale { get; private set; }

    protected override void OnLoadData(XmlNode data)
    {
        ModelType = Utility.Xml.GetAttribute<int>(data, "ModelType");
        Path = Utility.Xml.GetAttribute<string>(data, "Path");
        Scale = Utility.Xml.GetAttribute<string>(data, "Scale");
    }
    public Vector3 GetScale()
    {
        Vector3 scale = Vector3.zero;
        Utility.Xml.ParseString(Scale, Utility.Xml.SplitComma, ref scale);
        return scale;
    }

}