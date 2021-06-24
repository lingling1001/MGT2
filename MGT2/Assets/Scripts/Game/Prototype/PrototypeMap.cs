using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;
using UnityEngine;

public class PrototypeMap : BasePrototype
{
    public string Path { get; private set; }
    /// <summary>
    /// ��ͼ��С
    /// </summary>
    public string Size { get; private set; }
    public string StarInfo { get; private set; }
    public int GridSize { get; private set; }

    protected override void OnLoadData(XmlNode data)
    {
        Path = Utility.Xml.GetAttribute<string>(data, "Path");
        Size = Utility.Xml.GetAttribute<string>(data, "Size");
        StarInfo = Utility.Xml.GetAttribute<string>(data, "StarInfo");
        GridSize = Utility.Xml.GetAttribute<int>(data, "GridSize");

    }
    private int[] _arrsSize;
    /// <summary>
    /// ��ͼ��С
    /// </summary>
    public int[] GetSize()
    {
        if (_arrsSize == null)
        {
            _arrsSize = Utility.Xml.ParseString<int>(Size, Utility.Xml.SplitComma);
        }
        return _arrsSize;
    }
}