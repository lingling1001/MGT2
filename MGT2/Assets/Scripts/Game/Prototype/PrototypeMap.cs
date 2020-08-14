using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;
using UnityEngine;

public class PrototypeMap : BasePrototype
{
    public string Path { get; private set; }
    public Vector3 CameraAngle { get; private set; }
    public Vector3 CameraPosition { get; private set; }
    public float CameraFieldView { get; private set; }
    public string DragLimit { get; private set; }
    public string Size { get; private set; }

    public string StarInfo { get; private set; }

    protected override void OnLoadData(XmlNode data)
    {
        Path = Utility.Xml.GetAttribute<string>(data, "Path");
        CameraAngle = GetAttribut_vector3(data, "CameraAngle");
        CameraPosition = GetAttribut_vector3(data, "CameraPosition");
        CameraFieldView = Utility.Xml.GetAttribute<float>(data, "CameraFieldView");
        DragLimit = Utility.Xml.GetAttribute<string>(data, "DragLimit");
        Size = Utility.Xml.GetAttribute<string>(data, "Size");
        StarInfo = Utility.Xml.GetAttribute<string>(data, "StarInfo");
    }
    private float[] _arrLimit;
    /// <summary>
    /// ÍÏ¶¯ÏÞÖÆ 0min x 1max x 2min z 3max z
    /// </summary>
    /// <returns></returns>
    public float[] GetDragLimit()
    {
        if (_arrLimit == null)
        {
            _arrLimit = Utility.Xml.ParseString<float>(DragLimit, Utility.Xml.SplitComma);
        }
        return _arrLimit;
    }
    private int[] _arrsSize;
    public int[] GetSize()
    {
        if (_arrsSize == null)
        {
            _arrsSize = Utility.Xml.ParseString<int>(Size, Utility.Xml.SplitComma);
        }
        return _arrsSize;
    }
}