using System;
using System.Xml;
using System.Collections.Generic;
using MFrameWork;

public class PrototypeGameConfig : BasePrototype
{

    protected override void OnLoadData(XmlNode data)
    {

    }

    public static T[] GetConfigList<T>(EnumGameConfig type, char[] spilt)
    {
        PrototypeGameConfig data = null;
        if (TryGetPrototype(type, out data))
        {
            return Utility.Xml.ParseString<T>(data.Name, spilt);
        }
        return null;
    }

    public static T[] GetConfigList<T>(EnumGameConfig type)
    {
        return GetConfigList<T>(type, Utility.Xml.SplitComma);
    }
    public static int GetConfigInt(EnumGameConfig id)
    {
        int value = 0;
        TryGetConfigInt(id, out value);
        return value;
    }
    public static bool TryGetConfigInt(EnumGameConfig type, out int value)
    {
        value = 0;
        PrototypeGameConfig data = null;
        if (TryGetPrototype(type, out data))
        {
            if (int.TryParse(data.Name, out value))
            {
                return true;
            }
        }
        return false;
    }
    public static bool TryGetPrototype(EnumGameConfig type, out PrototypeGameConfig data)
    {
        data = PrototypeManager<PrototypeGameConfig>.Instance.GetPrototype((int)type);
        if (data == null)
        {
            return false;
        }
        return true;
    }
}


public enum EnumGameConfig : int
{
    /// <summary>
    /// д╛хо╫ги╚
    /// </summary>
    DefaultRole = 1,


}
