using MFrameWork;
using System.Collections.Generic;

public static class FactoryAttribute
{

    public static AttributeData CreateAttributeData(int id, int value)
    {
        return new AttributeData(id, value);

    }
  

    public static List<AttributeData> CreateList(int hp, int speed)
    {
        List<AttributeData> list = new List<AttributeData>();
        AttributeData dataHp = CreateAttributeData(DefineAttributeId.HP, hp);
        AttributeData dataSpeed = CreateAttributeData(DefineAttributeId.SPEED_MOVE, speed);
        list.Add(dataHp);
        list.Add(dataSpeed);
        return list;
    }

    public static List<AttributeData> CreateList(List<int[]> keyValues)
    {
        List<AttributeData> list = new List<AttributeData>();
        if (keyValues == null)
        {
            return list;
        }
        for (int cnt = 0; cnt < keyValues.Count; cnt++)
        {
            if (keyValues[cnt] == null || keyValues[cnt].Length != 2)
            {
                continue;
            }
            list.Add(CreateAttributeData(keyValues[cnt][0], keyValues[cnt][1]));
        }
        return list;
    }
    /// <summary>
    ///  分号分割 冒号分割 10,1|11,2
    /// </summary>
    public static List<AttributeData> CreateList(string strValues)
    {
        List<AttributeData> list = new List<AttributeData>();
        string[] keyValues = Utility.Xml.ParseString<string>(strValues, Utility.Xml.SplitVerticalBar);
        if (keyValues == null)
        {
            return list;
        }
        for (int cnt = 0; cnt < keyValues.Length; cnt++)
        {
            int[] keyValue = Utility.Xml.ParseString<int>(keyValues[cnt], Utility.Xml.SplitComma);
            if (keyValue != null && keyValues.Length == 2)
            {
                list.Add(CreateAttributeData(keyValue[0], keyValue[1]));
            }
        }
        return list;
    }
}